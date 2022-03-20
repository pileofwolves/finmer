/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a literal inline value.
    /// </summary>
    public partial class FormScriptValueLiteral : FormScriptNode
    {

        private ValueLiteral m_Literal;

        public FormScriptValueLiteral()
        {
            InitializeComponent();
        }

        private void FormScriptValueLiteral_Load(object sender, System.EventArgs e)
        {
            m_Literal = (ValueLiteral)Node;

            optTypeNum.Checked = m_Literal.LiteralType == ValueLiteral.ELiteralType.Number;
            optTypeBool.Checked = m_Literal.LiteralType == ValueLiteral.ELiteralType.Boolean;
            optTypeString.Checked = m_Literal.LiteralType == ValueLiteral.ELiteralType.String;
            optTypeNil.Checked = m_Literal.LiteralType == ValueLiteral.ELiteralType.Nil;

            nudValue.Value = (decimal)m_Literal.NumberValue;
            txtValue.Text = m_Literal.StringValue;
            chkValue.Checked = m_Literal.BooleanValue;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            m_Literal.NumberValue = (float)nudValue.Value;
            m_Literal.StringValue = txtValue.Text;
            m_Literal.BooleanValue = chkValue.Checked;
        }

        private void UpdateValueVisibility()
        {
            nudValue.Visible = optTypeNum.Checked;
            txtValue.Visible = optTypeString.Checked;
            chkValue.Visible = optTypeBool.Checked;
        }

        private void optTypeNum_CheckedChanged(object sender, System.EventArgs e)
        {
            if (optTypeNum.Checked)
            {
                m_Literal.LiteralType = ValueLiteral.ELiteralType.Number;
                UpdateValueVisibility();
            }
        }

        private void optTypeBool_CheckedChanged(object sender, System.EventArgs e)
        {
            if (optTypeBool.Checked)
            {
                m_Literal.LiteralType = ValueLiteral.ELiteralType.Boolean;
                UpdateValueVisibility();
            }
        }

        private void optTypeString_CheckedChanged(object sender, System.EventArgs e)
        {
            if (optTypeString.Checked)
            {
                m_Literal.LiteralType = ValueLiteral.ELiteralType.String;
                UpdateValueVisibility();
            }
        }

        private void optTypeNil_CheckedChanged(object sender, System.EventArgs e)
        {
            if (optTypeNil.Checked)
            {
                m_Literal.LiteralType = ValueLiteral.ELiteralType.Nil;
                UpdateValueVisibility();
            }
        }

    }

}
