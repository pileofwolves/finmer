﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    public partial class ScriptValueBoolEditor : UserControl
    {

        public ScriptValueBoolEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns a new value wrapper using the current UI state as input.
        /// </summary>
        public ValueWrapperBool GetValue()
        {
            if (optModeInlineLua.Checked)
            {
                return new ValueWrapperBool
                {
                    OperandMode = ValueWrapperBool.EOperandMode.Script,
                    OperandText = txtLua.Text
                };
            }
            else if (optModeVar.Checked)
            {
                return new ValueWrapperBool
                {
                    OperandMode = ValueWrapperBool.EOperandMode.Variable,
                    OperandText = txtVar.Text.MakeSafeIdentifier()
                };
            }
            else
            {
                return new ValueWrapperBool
                {
                    OperandMode = ValueWrapperBool.EOperandMode.Literal,
                    OperandLiteral = chkLiteral.Checked
                };
            }
        }

        /// <summary>
        /// Applies a value wrapper's settings to the UI.
        /// </summary>
        public void SetValue(ValueWrapperBool value)
        {
            switch (value.OperandMode)
            {
                case ValueWrapperBool.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    chkLiteral.Checked = value.OperandLiteral;
                    break;

                case ValueWrapperBool.EOperandMode.Variable:
                    optModeVar.Checked = true;
                    txtVar.Text = value.OperandText;
                    break;

                case ValueWrapperBool.EOperandMode.Script:
                    optModeInlineLua.Checked = true;
                    txtLua.Text = value.OperandText;
                    break;
            }
        }

        private void optModeLiteral_CheckedChanged(object sender, EventArgs e)
        {
            chkLiteral.Enabled = optModeLiteral.Checked;
        }

        private void optModeNumberVar_CheckedChanged(object sender, EventArgs e)
        {
            txtVar.Enabled = optModeVar.Checked;
        }

        private void optModeInlineLua_CheckedChanged(object sender, EventArgs e)
        {
            txtLua.Enabled = optModeInlineLua.Checked;
        }

    }

}
