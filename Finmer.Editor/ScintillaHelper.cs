/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Drawing;
using ScintillaNET;

namespace Finmer.Editor
{

    /// <summary>
    /// Utility for configuring a Scintilla instance at runtime.
    /// </summary>
    internal static class ScintillaHelper
    {

        /// <summary>
        /// Describes the language on display in the Scintilla editor.
        /// </summary>
        internal enum EScintillaStyle
        {
            PlainText,
            Lua
        }

        /// <summary>
        /// Configure the specified Scintilla editor.
        /// </summary>
        internal static void Setup(Scintilla scintilla, EScintillaStyle style)
        {
            // Basic visual configuration
            scintilla.WrapMode = WrapMode.Word;
            scintilla.WrapIndentMode = WrapIndentMode.Indent;
            scintilla.WrapVisualFlags = WrapVisualFlags.End;
            scintilla.CaretLineVisible = true;
            scintilla.CaretLineBackColor = Color.LightSkyBlue;
            scintilla.CaretLineBackColorAlpha = 32;

            // Multi-selection configuration
            scintilla.AdditionalSelectionTyping = true;
            scintilla.MultiPaste = MultiPaste.Each;

            // Show line numbers
            scintilla.Margins[0].Type = MarginType.Number;
            scintilla.Margins[0].Width = 32;

            // Default plain-text style
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = @"Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].ForeColor = Color.Black;

            // If the editor is for Lua code, then additional styling is needed
            if (style == EScintillaStyle.PlainText)
                return;

            // Configure keyword lists
            scintilla.Lexer = Lexer.Lua;
            scintilla.SetKeywords(0, @"and break do else elseif end false for function if in local nil not or repeat return then true until while");
            scintilla.SetKeywords(1, @"assert collectgarbage error getmetatable ipairs next pairs pcall print rawequal rawget rawset require select setmetatable tonumber tostring type unpack xpcall coroutine.create coroutine.resume coroutine.running coroutine.status coroutine.wrap coroutine.yield math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.cosh math.deg math.exp math.floor math.fmod math.frexp math.huge math.ldexp math.log math.log10 math.max math.min math.modf math.pi math.pow math.rad math.random math.sin math.sinh math.sqrt math.tan math.tanh string.byte string.char string.dump string.find string.format string.gmatch string.gsub string.len string.lower string.match string.rep string.reverse string.sub string.upper table.concat table.insert table.maxn table.remove table.sort");
            scintilla.SetKeywords(2, @"AddButton AddLink AddParticipant AddTag AdvanceTime AwardXP Begin CanGrapple CanSwallow ClearLog Combat2 Creature End EndGame GetActiveCombat GetGrappler GetGrapplingWith GetPredator GetPredator GetTime GiveItem HasItem HasTag IsDead IsDebugMode IsGrappleInitiator IsGrappling IsSwallowed Item Journal.Close Journal.Update Log LogRaw LogSplit ModifyMoney OnCombatEnd OnCreatureKilled OnCreatureReleased OnCreatureVored OnPlayerKilled OnRoundStart OnRoundEnd Player RemoveTag SaveData.IsRestoringGame SaveData.ShowSaveDialog SaveData.TakeCheckpoint SetButtonWidth SetGrappling SetInstruction SetInventoryEnabled SetLocation SetScene SetVored Shop Sleep Storage.GetFlag Storage.GetNumber Storage.GetString Storage.ModifyNumber Storage.SetFlag Storage.SetNumber Storage.SetString TakeItem Text.GetString Text.SetContext Text.SetVariable UnsetGrappling UnsetVored");

            // Configure syntax highlighting
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
