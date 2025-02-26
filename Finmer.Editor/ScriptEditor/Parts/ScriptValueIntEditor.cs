/*
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

    public partial class ScriptValueIntEditor : UserControl
    {

        public ScriptValueIntEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns a new value wrapper using the current UI state as input.
        /// </summary>
        public ValueWrapperInt GetValue()
        {
            if (optModeInlineLua.Checked)
            {
                return new ValueWrapperInt
                {
                    OperandMode = ValueWrapperInt.EOperandMode.Script,
                    OperandText = txtLua.Text
                };
            }
            else if (optModeNumberVar.Checked)
            {
                return new ValueWrapperInt
                {
                    OperandMode = ValueWrapperInt.EOperandMode.Variable,
                    OperandText = txtNumberVar.Text.MakeSafeIdentifier()
                };
            }
            else
            {
                return new ValueWrapperInt
                {
                    OperandMode = ValueWrapperInt.EOperandMode.Literal,
                    OperandLiteral = (int)nudOperand.Value
                };
            }
        }

        /// <summary>
        /// Applies a value wrapper's settings to the UI.
        /// </summary>
        public void SetValue(ValueWrapperInt value)
        {
            switch (value.OperandMode)
            {
                case ValueWrapperInt.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    nudOperand.Value = value.OperandLiteral;
                    break;

                case ValueWrapperInt.EOperandMode.Variable:
                    optModeNumberVar.Checked = true;
                    txtNumberVar.Text = value.OperandText;
                    break;

                case ValueWrapperInt.EOperandMode.Script:
                    optModeInlineLua.Checked = true;
                    txtLua.Text = value.OperandText;
                    break;
            }
        }

        private void optModeLiteral_CheckedChanged(object sender, EventArgs e)
        {
            nudOperand.Enabled = optModeLiteral.Checked;
        }

        private void optModeNumberVar_CheckedChanged(object sender, EventArgs e)
        {
            txtNumberVar.Enabled = optModeNumberVar.Checked;
        }

        private void optModeInlineLua_CheckedChanged(object sender, EventArgs e)
        {
            txtLua.Enabled = optModeInlineLua.Checked;
        }

    }

}
