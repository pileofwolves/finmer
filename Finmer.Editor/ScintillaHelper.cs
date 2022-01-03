/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Drawing;
using ScintillaNET;

namespace Finmer.Editor
{

    internal static class ScintillaHelper
    {

        internal static void Setup(Scintilla scintilla, bool lua = true)
        {
            // misc setitngs
            scintilla.WrapMode = WrapMode.Word;
            scintilla.WrapIndentMode = WrapIndentMode.Indent;
            scintilla.WrapVisualFlags = WrapVisualFlags.Margin;

            scintilla.CaretLineVisible = true;
            scintilla.CaretLineBackColor = Color.LightSkyBlue;
            scintilla.CaretLineBackColorAlpha = 32;

            scintilla.Margins[0].Type = MarginType.Number;
            scintilla.Margins[0].Width = 32;

            // set up default style
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].ForeColor = Color.Black;

            if (!lua) return;

            // set up keyword lists
            scintilla.Lexer = Lexer.Lua;
            scintilla.SetKeywords(0, "and break do else elseif end false for function if in local nil not or repeat return then true until while");
            scintilla.SetKeywords(1, "assert collectgarbage error getmetatable ipairs next pairs pcall print rawequal rawget rawset require select setmetatable tonumber tostring type unpack xpcall coroutine.create coroutine.resume coroutine.running coroutine.status coroutine.wrap coroutine.yield math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.cosh math.deg math.exp math.floor math.fmod math.frexp math.huge math.ldexp math.log math.log10 math.max math.min math.modf math.pi math.pow math.rad math.random math.sin math.sinh math.sqrt math.tan math.tanh string.byte string.char string.dump string.find string.format string.gmatch string.gsub string.len string.lower string.match string.rep string.reverse string.sub string.upper table.concat table.insert table.maxn table.remove table.sort");
            scintilla.SetKeywords(2, "Log LogRaw LogGsub LogSplit ClearLog SaveGame GetString AdvanceTime GetTime Sleep SaveGame SetScene SetInstruction SetLocation SetButtonWidth SetInventoryEnabled AddButton AddLink Creature Item Shop Storage.SetFlag Storage.SetNumber Storage.SetString Storage.ModifyNumber Storage.GetFlag Storage.GetNumber Storage.GetString Player AddTag RemoveTag HasTag IsGrappling IsEaten IsEatenBy GetGrappler GetPredator HasItem GiveItem TakeItem AwardXP Combat.Begin Combat.End Combat.Reset Combat.Interrupt Combat.Resume Combat.AddCombatant Combat.SetVored Combat.SetGrappling Combat.SetPinned Combat.OnRoundEnd Combat.OnCombatEnd Combat.OnCharacterKilled Combat.OnCharacterVored Combat.OnCharacterDigested Combat.OnCharacterReleased Combat.OnPlayerKilled Combat.OnPlayerDigested Journal.Update Journal.Close Text.SetGlobalSubstitute Text.SetContext");

            // apply special colorations to some objects
            scintilla.StyleClearAll();
            scintilla.Styles[Style.Lua.Comment].ForeColor = Color.ForestGreen;
            scintilla.Styles[Style.Lua.CommentLine].ForeColor = Color.ForestGreen;
            scintilla.Styles[Style.Lua.CommentDoc].ForeColor = Color.ForestGreen;
            scintilla.Styles[Style.Lua.Number].ForeColor = Color.Purple;
            scintilla.Styles[Style.Lua.String].ForeColor = Color.DarkRed;
            scintilla.Styles[Style.Lua.LiteralString].ForeColor = Color.DarkRed;
            scintilla.Styles[Style.Lua.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Lua.Word].Bold = true;
            scintilla.Styles[Style.Lua.Word2].ForeColor = Color.SteelBlue;
            scintilla.Styles[Style.Lua.Word3].ForeColor = Color.DarkSlateBlue;
        }

    }

}
