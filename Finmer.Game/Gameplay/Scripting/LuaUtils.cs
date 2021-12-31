/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using System.Windows.Media;
using JetBrains.Annotations;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Collection of various utilities and application-specific extensions for the Lua API.
    /// </summary>
    internal static class LuaUtils
    {

        /// <summary>
        /// Converts the value at the specified index on the Lua stack into a Color.
        /// </summary>
        public static Color lua_tocolor(IntPtr L, int index)
        {
            if (lua_type(L, index) != ELuaType.Table)
                throw new ArgumentException("Object at specified index is not a table.", nameof(index));

            lua_getfield(L, index, "r");
            lua_getfield(L, index, "g");
            lua_getfield(L, index, "b");
            Color ret = Color.FromRgb((byte)lua_tonumber(L, -3), (byte)lua_tonumber(L, -2), (byte)lua_tonumber(L, -1));
            lua_pop(L, 3);
            return ret;
        }

        /// <summary>
        /// Returns a string containing an overview of the contents of the specified Lua stack.
        /// </summary>
        /// <param name="state">The Lua stack to examine. May be the main thread, or a coroutine.</param>
        [UsedImplicitly]
        public static string lua_stackdump(IntPtr state)
        {
            var output = new StringBuilder();
            int count = lua_gettop(state);
            for (int i = 1; i <= count; i++)
            {
                // Write a line identifying the value type at this stack index
                output.AppendLine($"[{i:D02}] {lua_describe(state, i)}");
            }

            return output.ToString();
        }

        /// <summary>
        /// Returns a string describing the type and value of the specified Lua stack slot.
        /// </summary>
        /// <param name="state">The Lua stack to examine. May be the main thread, or a coroutine.</param>
        /// <param name="i">The stack index.</param>
        [UsedImplicitly]
        public static string lua_describe(IntPtr state, int i)
        {
            var type = lua_type(state, i);
            var typename = lua_typename(state, type);

            switch (type)
            {
                case ELuaType.Boolean:
                    return $"{typename}: {(lua_toboolean(state, i) ? "true" : "false")}";

                case ELuaType.Number:
                    return $"{typename}: {lua_tonumber(state, i):F3}";

                case ELuaType.String:
                    return $"{typename}: \"{lua_tostring(state, i)}\"";

                case ELuaType.Userdata:
                case ELuaType.LightUserdata:
                case ELuaType.Thread:
                case ELuaType.Table:
                case ELuaType.Function:
                    return $"{typename}: 0x{lua_topointer(state, i).ToString("X16")}";

                default:
                    return typename;
            }
        }

    }

}
