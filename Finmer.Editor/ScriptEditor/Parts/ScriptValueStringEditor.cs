using System;
using System.Windows.Forms;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    public partial class ScriptValueStringEditor : UserControl
    {

        public ScriptValueStringEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns a new value wrapper using the current UI state as input.
        /// </summary>
        public ValueWrapperString GetValue()
        {
            if (optModeInlineLua.Checked)
            {
                return new ValueWrapperString
                {
                    OperandMode = ValueWrapperString.EOperandMode.Script,
                    OperandText = txtLua.Text
                };
            }
            else if (optModeVar.Checked)
            {
                return new ValueWrapperString
                {
                    OperandMode = ValueWrapperString.EOperandMode.Variable,
                    OperandText = txtVar.Text
                };
            }
            else
            {
                return new ValueWrapperString
                {
                    OperandMode = ValueWrapperString.EOperandMode.Literal,
                    OperandText = txtLiteral.Text
                };
            }
        }

        /// <summary>
        /// Applies a value wrapper's settings to the UI.
        /// </summary>
        public void SetValue(ValueWrapperString value)
        {
            switch (value.OperandMode)
            {
                case ValueWrapperString.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    txtLiteral.Text = value.OperandText;
                    break;

                case ValueWrapperString.EOperandMode.Variable:
                    optModeVar.Checked = true;
                    txtVar.Text = value.OperandText;
                    break;

                case ValueWrapperString.EOperandMode.Script:
                    optModeInlineLua.Checked = true;
                    txtLua.Text = value.OperandText;
                    break;
            }
        }

        private void optModeLiteral_CheckedChanged(object sender, EventArgs e)
        {
            txtLiteral.Enabled = optModeLiteral.Checked;
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
