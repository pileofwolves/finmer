/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Exposes the native Lua API through P/Invoke.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4200", Justification = "Lua already performs argument checking and error handling")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4214", Justification = "Lua already performs argument checking and error handling")]
    public static class LuaApi
    {

        [UnmanagedFunctionPointer(LUA_CALLING_CONVENTION)]
        public delegate int lua_CFunction(IntPtr L);

        [UnmanagedFunctionPointer(LUA_CALLING_CONVENTION)]
        public delegate int lua_Writer(IntPtr L, IntPtr chunk, UIntPtr chunklen, IntPtr userdata);

        /// <summary>
        /// Represents a Lua value type. Matches the macros in lua.h.
        /// </summary>
        public enum ELuaType
        {
            None = -1,

            Nil = 0,
            Boolean = 1,
            LightUserdata = 2,
            Number = 3,
            String = 4,
            Table = 5,
            Function = 6,
            Userdata = 7,
            Thread = 8
        }

        private const string LUA_DLL = "Lua51.dll";
        private const CallingConvention LUA_CALLING_CONVENTION = CallingConvention.Cdecl;

        public const int LUA_REGISTRYINDEX = -10000;
        public const int LUA_ENVIRONINDEX = -10001;
        public const int LUA_GLOBALSINDEX = -10002;

        public const int LUA_GCSTOP = 0;
        public const int LUA_GCRESTART = 1;
        public const int LUA_GCCOLLECT = 2;
        public const int LUA_GCCOUNT = 3;
        public const int LUA_GCCOUNTB = 4;
        public const int LUA_GCSTEP = 5;
        public const int LUA_GCSETPAUSE = 6;
        public const int LUA_GCSETSTEPMUL = 7;

        public const int LUA_MULTRET = -1;
        public const int LUA_YIELD = 1;

        private static IntPtr s_NativeLibraryHandle = IntPtr.Zero;

        /// <summary>
        /// Load the platform-appropriate Lua library. Note that we never unload the library, because there is no need to.
        /// </summary>
        public static void LoadNativeLibrary()
        {
            if (s_NativeLibraryHandle != IntPtr.Zero)
                return;

            // Load either the 32-bit or 64-bit Lua runtime module, depending on the OS
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            s_NativeLibraryHandle = NativeMethods.LoadLibrary(Environment.Is64BitProcess
                ? Path.Combine(basePath, "x64", "Lua51.dll")
                : Path.Combine(basePath, "x86", "Lua51.dll"));
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_checkstack(IntPtr L, int extra);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_close(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_concat(IntPtr L, int n);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_createtable(IntPtr L, int narr, int nrec);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr userdata, [MarshalAs(UnmanagedType.Bool)] bool strip);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_equal(IntPtr L, int index1, int index2);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_error(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_gc(IntPtr L, int what, int data);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_getfield(IntPtr L, int index, [MarshalAs(UnmanagedType.LPStr)] string k);

        public static void lua_getglobal(IntPtr L, string name)
        {
            lua_getfield(L, LUA_GLOBALSINDEX, name);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_getmetatable(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_gettable(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_gettop(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_insert(IntPtr L, int index);

        public static bool lua_isboolean(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.Boolean;
        }

        public static bool lua_isnumber(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.Number;
        }

        public static bool lua_isstring(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.String;
        }

        public static bool lua_isnil(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.Nil;
        }

        public static bool lua_isnone(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.None;
        }

        public static bool lua_isnoneornil(IntPtr L, int index)
        {
            ELuaType slot_type = lua_type(L, index);
            return slot_type == ELuaType.Nil || slot_type == ELuaType.None;
        }

        public static bool lua_istable(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.Table;
        }

        public static bool lua_isfunction(IntPtr L, int index)
        {
            return lua_type(L, index) == ELuaType.Function;
        }

        public static bool lua_isuserdata(IntPtr L, int index)
        {
            ELuaType slot_type = lua_type(L, index);
            return slot_type == ELuaType.Userdata || slot_type == ELuaType.LightUserdata;
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_lessthan(IntPtr L, int index1, int index2);

        public static void lua_newtable(IntPtr L)
        {
            lua_createtable(L, 0, 0);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr lua_newthread(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr lua_newuserdata(IntPtr L, UIntPtr size);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_next(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern UIntPtr lua_objlen(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_call(IntPtr L, int nargs, int nresults);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc);

        public static void lua_pop(IntPtr L, int n)
        {
            lua_settop(L, -n - 1);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_pushboolean(IntPtr L, [MarshalAs(UnmanagedType.Bool)] bool value);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        private static extern void lua_pushcclosure(IntPtr L, lua_CFunction fn, int n);

        public static void lua_pushcfunction(IntPtr L, lua_CFunction f)
        {
            lua_pushcclosure(L, f, 0);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_pushlightuserdata(IntPtr L, IntPtr p);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        private static extern void lua_pushlstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string s, UIntPtr len);

        public static void lua_pushstring(IntPtr L, string s)
        {
            if (s == null)
                lua_pushnil(L);
            else
                lua_pushlstring(L, s, new UIntPtr(unchecked((ulong)s.Length)));
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_pushnil(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_pushnumber(IntPtr L, double n);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_pushthread(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_pushvalue(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool lua_rawequal(IntPtr L, int index1, int index2);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_rawget(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_rawgeti(IntPtr L, int index, int n);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_rawset(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_rawseti(IntPtr L, int index, int n);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_remove(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_replace(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_resume(IntPtr L, int narg);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_setfield(IntPtr L, int index, [MarshalAs(UnmanagedType.LPStr)] string k);

        public static void lua_setglobal(IntPtr L, string name)
        {
            lua_setfield(L, LUA_GLOBALSINDEX, name);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_setfenv(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_setmetatable(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_settable(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_settop(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_status(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool lua_toboolean(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern lua_CFunction lua_tocfunction(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern double lua_tonumber(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr lua_topointer(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        private static extern IntPtr lua_tolstring(IntPtr L, int index, out UIntPtr len);

        public static string lua_tostring(IntPtr L, int index)
        {
            // Per Lua documentation, tostring should return null for non-string stack items
            if (lua_type(L, index) != ELuaType.String)
                return null;

            // Convert the string's ANSI pointer to a managed string
            IntPtr stringPtr = lua_tolstring(L, index, out UIntPtr len);
            return Marshal.PtrToStringAnsi(stringPtr, checked((int)len.ToUInt32()));
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr lua_tothread(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr lua_touserdata(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern ELuaType lua_type(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCopyMarshaller))]
        public static extern string lua_typename(IntPtr L, ELuaType type);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void lua_xmove(IntPtr from, IntPtr to, int n);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int lua_yield(IntPtr L, int nresults);

        public static void luaL_getmetatable(IntPtr L, string name)
        {
            lua_getfield(L, LUA_REGISTRYINDEX, name);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaL_loadbuffer(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string s, UIntPtr size,
            [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION, EntryPoint = "luaL_loadbuffer")]
        public static extern int luaL_loadbuffer_binary(IntPtr L, [In] byte[] body, UIntPtr size, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaL_newmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string tname);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern IntPtr luaL_newstate();

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaopen_base(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaopen_table(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaopen_string(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaopen_math(IntPtr L);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaL_argerror(IntPtr L, int narg, [MarshalAs(UnmanagedType.LPStr)] string extramsg);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaL_typerror(IntPtr L, int narg, [MarshalAs(UnmanagedType.LPStr)] string tname);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern int luaL_ref(IntPtr L, int t);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void luaL_unref(IntPtr L, int t, int r);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void luaL_checktype(IntPtr L, int narg, ELuaType t);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern double luaL_checknumber(IntPtr L, int index);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        private static extern IntPtr luaL_checklstring(IntPtr L, int index, out UIntPtr size);

        public static string luaL_checkstring(IntPtr L, int index)
        {
            return Marshal.PtrToStringAnsi(luaL_checklstring(L, index, out UIntPtr size), (int)size);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        private static extern IntPtr luaL_optlstring(IntPtr L, int numArg, [MarshalAs(UnmanagedType.LPStr)] string def, out UIntPtr l);

        public static string luaL_optstring(IntPtr L, int index, string def)
        {
            return Marshal.PtrToStringAnsi(luaL_optlstring(L, index, def, out UIntPtr size), (int)size);
        }

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern double luaL_optnumber(IntPtr L, int nArg, double def);

        [DllImport(LUA_DLL, CallingConvention = LUA_CALLING_CONVENTION)]
        public static extern void luaL_where(IntPtr L, int lvl);

        public static int luaL_error(IntPtr L, string message)
        {
            luaL_where(L, 1);
            lua_pushstring(L, message);
            lua_concat(L, 2);
            return lua_error(L);
        }

        public static int lua_upvalueindex(int i)
        {
            return LUA_GLOBALSINDEX - i;
        }

        public static int lua_absindex(IntPtr L, int i)
        {
            return i > 0 || i <= LUA_REGISTRYINDEX ? i : lua_gettop(L) + i + 1;
        }

        private static class NativeMethods
        {

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibrary(string dllToLoad);

        }

        /// <summary>
        /// P/Invoke marshaller that copies returned ANSI string (const char*) into a managed string, without
        /// taking ownership of the native buffer (like UnmanagedType.LPStr does).
        /// </summary>
        private sealed class StringCopyMarshaller : ICustomMarshaler
        {

            private static StringCopyMarshaller s_Instance;

#pragma warning disable IDE0060 // Remove unused parameter
            [UsedImplicitly]
            public static ICustomMarshaler GetInstance([UsedImplicitly] string cookie)
            {
                return s_Instance ?? (s_Instance = new StringCopyMarshaller());
            }
#pragma warning restore IDE0060 // Remove unused parameter

            public object MarshalNativeToManaged(IntPtr pNativeData)
            {
                return Marshal.PtrToStringAnsi(pNativeData);
            }

            public IntPtr MarshalManagedToNative(object ManagedObj)
            {
                throw new NotSupportedException();
            }

            public void CleanUpNativeData(IntPtr pNativeData)
            {
                // No cleanup required
            }

            public void CleanUpManagedData(object ManagedObj)
            {
                throw new NotSupportedException();
            }

            public int GetNativeDataSize()
            {
                return -1;
            }

        }

    }

}
