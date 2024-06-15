/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing math utilities.
    /// </summary>
    internal static class MathScriptLibrary
    {

        private static Random s_RNG;

        private const int k_NumBits = 32;

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Initialize the script RNG with a time-based seed
            s_RNG = new Random();

            // bit32 library
            lua_createtable(state, 0, 9);
            context.RegisterFunction(@"band", ExportedBit32And);
            context.RegisterFunction(@"bnot", ExportedBit32Not);
            context.RegisterFunction(@"bor", ExportedBit32Or);
            context.RegisterFunction(@"btest", ExportedBit32Test);
            context.RegisterFunction(@"bxor", ExportedBit32Xor);
            context.RegisterFunction(@"lrotate", ExportedBit32RotateLeft);
            context.RegisterFunction(@"lshift", ExportedBit32ShiftLeft);
            context.RegisterFunction(@"rrotate", ExportedBit32RotateRight);
            context.RegisterFunction(@"rshift", ExportedBit32ShiftRight);
            lua_setglobal(state, @"bit32");

            // Replace the built-in RNG with our custom one
            lua_getglobal(state, @"math");
            lua_pushnil(state); // math.randomseed is not supported
            lua_setfield(state, -2, "randomseed");
            context.RegisterFunction(@"random", ExportedRandom);
            lua_pop(state, 1);
        }

        private static int ExportedBit32And(IntPtr L)
        {
            // Compute the bitwise AND of all input operands
            uint sum = UInt32.MaxValue;
            for (int i = 1, c = lua_gettop(L); i <= c; i++)
                sum &= (uint)luaL_checknumber(L, i);

            lua_pushnumber(L, sum);
            return 1;
        }

        private static int ExportedBit32Test(IntPtr L)
        {
            // Compute the bitwise AND of all input operands, and return a boolean indicating whether the computed value was non-zero
            uint sum = UInt32.MaxValue;
            for (int i = 1, c = lua_gettop(L); i <= c; i++)
                sum &= (uint)luaL_checknumber(L, i);

            lua_pushboolean(L, sum != 0);
            return 1;
        }

        private static int ExportedBit32Or(IntPtr L)
        {
            // Compute the bitwise OR of all input operands
            uint sum = 0;
            for (int i = 1, c = lua_gettop(L); i <= c; i++)
                sum |= (uint)luaL_checknumber(L, i);

            lua_pushnumber(L, sum);
            return 1;
        }

        private static int ExportedBit32Xor(IntPtr L)
        {
            // Compute the bitwise XOR of all input operands
            uint sum = 0;
            for (int i = 1, c = lua_gettop(L); i <= c; i++)
                sum ^= (uint)luaL_checknumber(L, i);

            lua_pushnumber(L, sum);
            return 1;
        }

        private static int ExportedBit32Not(IntPtr L)
        {
            lua_pushnumber(L, ~((uint)luaL_checknumber(L, 1)));
            return 1;
        }

        private static int ExportedBit32ShiftLeft(IntPtr L)
        {
            int shift = (int)luaL_checknumber(L, 2);
            if (shift >= k_NumBits)
            {
                // If the value is shifted outside the integer range, all bits are zero
                lua_pushnumber(L, 0);
            }
            else
            {
                // Otherwise, calculate the bitwise shift
                uint num = (uint)luaL_checknumber(L, 1);
                lua_pushnumber(L, num << shift);
            }
            return 1;
        }

        private static int ExportedBit32ShiftRight(IntPtr L)
        {
            int shift = (int)luaL_checknumber(L, 2);
            if (shift >= k_NumBits)
            {
                // If the value is shifted outside the integer range, all bits are zero
                lua_pushnumber(L, 0);
            }
            else
            {
                // Otherwise, calculate the bitwise shift
                uint num = (uint)luaL_checknumber(L, 1);
                lua_pushnumber(L, num >> shift);
            }
            return 1;
        }

        private static int BitRotateInternal(IntPtr L, int shift)
        {
            shift &= k_NumBits - 1;
            if (shift != 0)
            {
                // 32-bit bitwise rotation
                uint num = (uint)luaL_checknumber(L, 1);
                lua_pushnumber(L, (num << shift) | (num >> (k_NumBits - shift)));
            }
            else
            {
                // A shift of zero bits is undefined, but logically means a no-op, so copy the input to the output
                lua_pushvalue(L, 1);
            }

            return 1;
        }

        private static int ExportedBit32RotateLeft(IntPtr L)
        {
            return BitRotateInternal(L, (int)luaL_checknumber(L, 2));
        }

        private static int ExportedBit32RotateRight(IntPtr L)
        {
            return BitRotateInternal(L, -(int)luaL_checknumber(L, 2));
        }

        private static int ExportedRandom(IntPtr L)
        {
            // Re-implementation of Lua's default math.random function, but with a much higher-quality underlying RNG
            double rand = s_RNG.NextDouble();
            switch (lua_gettop(L))
            {
                case 0:
                {
                    lua_pushnumber(L, rand);
                    return 1;
                }
                case 1:
                {
                    var upper = (int)luaL_checknumber(L, 1);
                    if (upper < 1)
                        return luaL_error(L, "interval is empty");
                    lua_pushnumber(L, Math.Floor(rand * upper) + 1);
                    return 1;
                }
                case 2:
                {
                    var lower = (int)luaL_checknumber(L, 1);
                    var upper = (int)luaL_checknumber(L, 2);
                    if (lower > upper)
                        return luaL_error(L, "interval is empty");
                    lua_pushnumber(L, Math.Floor(rand * (upper - lower + 1)) + lower);
                    return 1;
                }
                default:
                    return luaL_error(L, "too many arguments");
            }
        }

    }

}
