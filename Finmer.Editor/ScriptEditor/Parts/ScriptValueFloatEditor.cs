/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    public partial class ScriptValueFloatEditor : UserControl
    {

        public ScriptValueFloatEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns a new value wrapper using the current UI state as input.
        /// </summary>
        public ValueWrapperFloat GetValue()
        {
            if (optModeInlineLua.Checked)
            {
                return new ValueWrapperFloat
                {
                    OperandMode = ValueWrapperFloat.EOperandMode.Script,
                    OperandText = txtLua.Text
                };
            }
            else if (optModeNumberVar.Checked)
            {
                return new ValueWrapperFloat
                {
                    OperandMode = ValueWrapperFloat.EOperandMode.Variable,
                    OperandText = txtNumberVar.Text.ToUpperInvariant()
                };
            }
            else
            {
                return new ValueWrapperFloat
                {
                    OperandMode = ValueWrapperFloat.EOperandMode.Literal,
                    OperandLiteral = (float)nudOperand.Value
                };
            }
        }

        /// <summary>
        /// Applies a value wrapper's settings to the UI.
        /// </summary>
        public void SetValue(ValueWrapperFloat value)
        {
            switch (value.OperandMode)
            {
                case ValueWrapperFloat.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    nudOperand.Value = (decimal)value.OperandLiteral;
                    break;

                case ValueWrapperFloat.EOperandMode.Variable:
                    optModeNumberVar.Checked = true;
                    txtNumberVar.Text = value.OperandText;
                    break;

                case ValueWrapperFloat.EOperandMode.Script:
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
