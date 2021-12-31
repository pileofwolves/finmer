/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Finmer.Core;
using Finmer.Gameplay.Scripting;
using Finmer.ViewModels;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an object that can be manipulated from Lua script.
    /// </summary>
    public abstract class ScriptableObject : BaseProp
    {

        private static readonly Dictionary<Type, Func<IntPtr, object, int>> s_MarshalFunctions =
            new Dictionary<Type, Func<IntPtr, object, int>>
            {
                { typeof(bool), LuaMarshalBoolean },
                { typeof(int), LuaMarshalInt },
                { typeof(float), LuaMarshalFloat },
                { typeof(string), LuaMarshalString }
            };

        private static readonly Dictionary<Type, Func<IntPtr, int, object>> s_UnmarshalFunctions =
            new Dictionary<Type, Func<IntPtr, int, object>>
            {
                { typeof(bool), LuaUnmarshalBoolean },
                { typeof(int), LuaUnmarshalInt },
                { typeof(float), LuaUnmarshalFloat },
                { typeof(string), LuaUnmarshalString }
            };

        /// <summary>
        /// This object's globally unique ID.
        /// </summary>
        public Guid ID { get; }

        private List<string> Tags { get; } = new List<string>();

        /// <summary>
        /// The Lua context in which this object was created.
        /// </summary>
        protected ScriptContext ScriptContext { get; }

        private readonly Dictionary<string, ExportedMethod> m_ExportedMethods = new Dictionary<string, ExportedMethod>();
        private readonly Dictionary<string, ExportedProperty> m_ExportedProperties = new Dictionary<string, ExportedProperty>();

        protected ScriptableObject(ScriptContext context)
        {
            ScriptContext = context;
            ID = Guid.NewGuid();

            CacheExportedMembers();
        }

        protected ScriptableObject(ScriptContext context, PropertyBag template)
        {
            ScriptContext = context;

            // Read the object ID
            byte[] id_bytes = template.GetBytes(SaveData.k_Object_Guid);
            ID = id_bytes == null || id_bytes.Length != 16
                ? Guid.NewGuid()
                : new Guid(id_bytes);

            // Read tags
            for (var i = 0; i < template.GetInt(SaveData.k_Object_TagCount); i++)
                Tags.Add(template.GetString(SaveData.k_Object_TagBase + i).ToUpperInvariant());

            CacheExportedMembers();
        }

        /// <summary>
        /// Adds a tag to this object's tag collection.
        /// </summary>
        public void AddTag(string tag)
        {
            if (String.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));
            Tags.Add(tag.ToUpperInvariant());
        }

        /// <summary>
        /// Removes a specified tag from the collection.
        /// </summary>
        public void RemoveTag(string tag)
        {
            if (String.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));
            Tags.Remove(tag.ToUpperInvariant());
        }

        /// <summary>
        /// Returns a value indicating whether this object has the specified tag.
        /// </summary>
        public bool HasTag(string tag)
        {
            if (String.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));
            return Tags.Contains(tag.ToUpperInvariant());
        }

        /// <summary>
        /// Saves the <see cref="ScriptableObject" />'s properties to a new <see cref="PropertyBag" /> and returns the result.
        /// </summary>
        public virtual PropertyBag SerializeProperties()
        {
            var output = new PropertyBag();

            // Object ID
            output.SetBytes(SaveData.k_Object_Guid, ID.ToByteArray());

            // Tag collection
            output.SetInt(SaveData.k_Object_TagCount, Tags.Count);
            for (var i = 0; i < Tags.Count; i++)
                output.SetString(SaveData.k_Object_TagBase + i, Tags[i]);

            return output;
        }

        /// <summary>
        /// Recovers a ScriptableObject from a userdatum on the Lua stack.
        /// </summary>
        /// <returns>
        /// Recovered ScriptableObject, or null if the object at the specified stack index is invalid.
        /// </returns>
        /// <param name="state">The Lua state to inspect.</param>
        /// <param name="stackIndex">The acceptable index on the stack at which the userdatum is located.</param>
        private static ScriptableObject FromLua(IntPtr state, int stackIndex)
        {
            // This function must be called on a userdatum
            if (LuaApi.lua_type(state, stackIndex) != LuaApi.ELuaType.Userdata)
                return null;

            // Retrieve the object ID from the userdatum
            byte[] buffer = new byte[16];
            IntPtr source_pointer = LuaApi.lua_touserdata(state, stackIndex);
            Marshal.Copy(source_pointer, buffer, 0, 16);
            var object_guid = new Guid(buffer);

            // Return the SO associated with that ID
            ScriptContext context = ScriptContext.FromLua(state);
            return context.GetPinnedObject(object_guid);
        }

        /// <summary>
        /// Works like FromLua(), but attempts to cast the recovered ScriptableObject to a derived class.
        /// </summary>
        /// <returns>
        /// Recovered ScriptableObject, or null if the object at the specified stack index is invalid or if the cast is invalid.
        /// </returns>
        public static T FromLuaOptional<T>(IntPtr state, int stackIndex) where T : ScriptableObject
        {
            return FromLua(state, stackIndex) as T;
        }

        /// <summary>
        /// Works like FromLua(), but casts the recovered ScriptableObject to a derived class. Throws a Lua error if this is not possible.
        /// </summary>
        /// <returns>
        /// Recovered ScriptableObject, or null if the object at the specified stack index is invalid.
        /// </returns>
        public static T FromLuaNonOptional<T>(IntPtr state, int stackIndex) where T : ScriptableObject
        {
            var obj = FromLua(state, stackIndex);
            if (obj == null)
            {
                LuaApi.luaL_typerror(state, stackIndex, typeof(T).Name);
                return null;
            }

            var derived = obj as T;
            if (derived == null)
            {
                LuaApi.luaL_argerror(state, stackIndex, $"expected {typeof(T).Name}, but got {obj.GetType().Name}");
                return null;
            }

            return derived;
        }

        /// <summary>
        /// Pushes a new userdatum on a Lua stack that exposes this ScriptableObject's scriptable properties and methods.
        /// </summary>
        /// <param name="state">The stack to push the userdatum on. May be a main thread or a coroutine.</param>
        public void PushToLua(IntPtr state)
        {
            // Make sure this object remains alive until the Lua runtime no longer references it
            ScriptContext.PinObject(this);

            // Allocate a userdatum of 16 bytes, which will contain the ScriptableObject UUID
            IntPtr native_block = LuaApi.lua_newuserdata(state, new UIntPtr(16U));

            // Reuse the same metatable for all ScriptableObjects
            if (LuaApi.luaL_newmetatable(state, @"ScriptableObject") == 1)
            {
                // If this is the first usage of the metatable, populate it with the metamethods we're interested in
                Debug.Assert(state == ScriptContext.LuaState, "Currently expecting this to run on the main stack; otherwise this code needs changes.");
                ScriptContext.PushDelegate(LuaMetaGet);
                LuaApi.lua_setfield(state, -2, @"__index");
                ScriptContext.PushDelegate(LuaMetaSet);
                LuaApi.lua_setfield(state, -2, @"__newindex");
                ScriptContext.PushDelegate(LuaMetaGC);
                LuaApi.lua_setfield(state, -2, @"__gc");
                ScriptContext.PushDelegate(LuaMetaEquality);
                LuaApi.lua_setfield(state, -2, @"__eq");
            }

            // Fill the userdatum memory with the GUID bytes
            byte[] guid_bytes = ID.ToByteArray();
            Marshal.Copy(guid_bytes, 0, native_block, 16);

            // Attach the metatable to the userdatum
            LuaApi.lua_setmetatable(state, -2);
        }


        /// <summary>
        /// Obtains exported properties and methods for this object through reflection, and caches the data.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Protected member functions are intended to be invoked through Lua (using reflection)")]
        private void CacheExportedMembers()
        {
            Debug.Assert(m_ExportedProperties.Count == 0);
            Debug.Assert(m_ExportedMethods.Count == 0);

            // Find exported properties (public properties tagged with ScriptablePropertyAttribute)
            IEnumerable<ExportedProperty> exported_props = GetType().GetProperties()
                .Select(prop => new ExportedProperty
                {
                    m_Property = prop,
                    m_Attribute = prop
                        .GetCustomAttributes<ScriptablePropertyAttribute>()
                        .FirstOrDefault()
                })
                .Where(item => item.m_Attribute != null);

            // Find exported methods (public static methods named Exported...() and tagged with ScriptableFunctionAttribute)
            var all_methods = GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.NonPublic);
            IEnumerable<ExportedMethod> exported_methods = all_methods
                .Where(method => method.GetCustomAttribute<ScriptableFunctionAttribute>() != null)
                .Select(method => new ExportedMethod
                {
                    m_Method = method,
                    m_Delegate = (LuaApi.lua_CFunction)method.CreateDelegate(typeof(LuaApi.lua_CFunction))
                })
                .Where(item => item.m_Method.Name.StartsWith("Exported"));

            // Build the hash-maps with the found objects
            foreach (ExportedProperty item in exported_props)
                m_ExportedProperties.Add(item.m_Property.Name, item);
            foreach (ExportedMethod item in exported_methods)
                m_ExportedMethods.Add(item.m_Method.Name.Substring(8), item); // Strip 'Exported' prefix
        }

        private static int LuaMetaEquality(IntPtr L)
        {
            ScriptableObject lhs = FromLua(L, 1);
            ScriptableObject rhs = FromLua(L, 2);
            LuaApi.lua_pushboolean(L, lhs.ID == rhs.ID);

            return 1;
        }

        private static int LuaMetaGet(IntPtr state)
        {
            ScriptableObject self = FromLua(state, 1);
            string key = LuaApi.luaL_checkstring(state, 2);

            // Look for a matching exported property
            if (self.m_ExportedProperties.TryGetValue(key, out ExportedProperty exported_property))
            {
                // Validate that read access is permitted
                if (!exported_property.m_Attribute.Access.HasFlag(EScriptAccess.Read))
                    return LuaApi.luaL_error(state, $"Cannot read from write-only property '{key}'");

                Type type = exported_property.m_Property.PropertyType;
                object value = exported_property.m_Property.GetValue(self);

                // Hardcoded handler: enum atoms can be converted to numbers
                if (type.IsEnum)
                {
                    LuaApi.lua_pushnumber(state, (int)value);
                    return 1;
                }

                // Hardcoded handler: all derived types of ScriptableObject
                if (type.IsSubclassOf(typeof(ScriptableObject)))
                {
                    var obj = (ScriptableObject)value;
                    if (obj != null)
                        obj.PushToLua(state);
                    else
                        LuaApi.lua_pushnil(state);

                    return 1;
                }

                // Otherwise, look for and invoke a marshaller for this atom
                if (s_MarshalFunctions.TryGetValue(type, out Func<IntPtr, object, int> marshaller))
                    return marshaller(state, value);

                // No valid marshaller is found
                return LuaApi.luaL_error(state, $"No marshaller found for property '{key}' of type '{type.FullName}'");
            }

            // Next, look for a matching exported method
            if (self.m_ExportedMethods.TryGetValue(key, out ExportedMethod exported_method))
            {
                // Push the function on the Lua stack. Note: We do not use the pinning mechanism here (ScriptContext::PinDelegate)
                // because for better performance we can cache the delegate ourselves in the ExportedMethod struct, and reuse it.
                LuaApi.lua_pushcfunction(state, exported_method.m_Delegate);
                return 1;
            }

            // No value found
            LuaApi.lua_pushnil(state);
            return 1;
        }

        private static int LuaMetaSet(IntPtr state)
        {
            ScriptableObject self = FromLua(state, 1);
            string key = LuaApi.luaL_checkstring(state, 2);

            // Look for a matching exported property
            if (self.m_ExportedProperties.TryGetValue(key, out ExportedProperty exported_property))
            {
                // Validate that read access is permitted
                if (!exported_property.m_Attribute.Access.HasFlag(EScriptAccess.Write))
                    return LuaApi.luaL_error(state, $"Cannot write to read-only property '{key}'");

                Type type = exported_property.m_Property.PropertyType;
                object new_value;

                // Find the correct unmarshaller. Inverse of LuaMetaGet().
                if (type.IsEnum)
                    new_value = (int)LuaApi.luaL_checknumber(state, 3);
                else if (type.IsSubclassOf(typeof(ScriptableObject)))
                    new_value = FromLua(state, 3);
                else if (s_UnmarshalFunctions.TryGetValue(type, out Func<IntPtr, int, object> unmarshaller))
                    new_value = unmarshaller(state, 3);
                else
                    return LuaApi.luaL_error(state, $"No unmarshaller found for property '{key}' of type '{type.FullName}'");

                // Assign the newly found value
                exported_property.m_Property.SetValue(self, new_value);
                return 0;
            }

            return LuaApi.luaL_error(state, $"Cannot write to unknown property '{key}'");
        }

        private static int LuaMetaGC(IntPtr state)
        {
            ScriptContext context = ScriptContext.FromLua(state);
            ScriptableObject sobj = FromLua(state, 1);
            context.UnpinObject(sobj);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedAddTag(IntPtr state)
        {
            var self = FromLua(state, 1);
            self.Tags.Add(LuaApi.luaL_checkstring(state, 2).ToUpperInvariant());
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedRemoveTag(IntPtr state)
        {
            var self = FromLua(state, 1);
            self.Tags.Remove(LuaApi.luaL_checkstring(state, 2).ToUpperInvariant());
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedHasTag(IntPtr state)
        {
            var self = FromLua(state, 1);
            LuaApi.lua_pushboolean(state, self.HasTag(LuaApi.luaL_checkstring(state, 2)));
            return 1;
        }

        private struct ExportedProperty
        {
            public PropertyInfo m_Property;
            public ScriptablePropertyAttribute m_Attribute;
        }

        private struct ExportedMethod
        {
            public MethodInfo m_Method;
            public LuaApi.lua_CFunction m_Delegate;
        }

        #region Lua marshalling

        private static int LuaMarshalBoolean(IntPtr state, object input)
        {
            LuaApi.lua_pushboolean(state, (bool)input);
            return 1;
        }

        private static int LuaMarshalInt(IntPtr state, object input)
        {
            LuaApi.lua_pushnumber(state, (int)input);
            return 1;
        }

        private static int LuaMarshalFloat(IntPtr state, object input)
        {
            LuaApi.lua_pushnumber(state, (float)input);
            return 1;
        }

        private static int LuaMarshalString(IntPtr state, object input)
        {
            LuaApi.lua_pushstring(state, (string)input);
            return 1;
        }

        private static object LuaUnmarshalBoolean(IntPtr state, int index)
        {
            return LuaApi.lua_toboolean(state, index);
        }

        private static object LuaUnmarshalInt(IntPtr state, int index)
        {
            return (int)LuaApi.lua_tonumber(state, index);
        }

        private static object LuaUnmarshalFloat(IntPtr state, int index)
        {
            return (float)LuaApi.lua_tonumber(state, index);
        }

        private static object LuaUnmarshalString(IntPtr state, int index)
        {
            return LuaApi.lua_tostring(state, index);
        }

        #endregion

    }

}
