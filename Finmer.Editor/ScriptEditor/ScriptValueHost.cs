/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents an editor control for ScriptValue objects.
    /// </summary>
    public partial class ScriptValueHost : UserControl
    {

        private ScriptValue m_Value;

        /// <summary>
        /// The ScriptValue that is being edited in this control.
        /// </summary>
        public ScriptValue Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                UpdateValueDisplay();
            }
        }

        public ScriptValueHost()
        {
            InitializeComponent();
        }

        private void UpdateValueDisplay()
        {
            lklText.Text = m_Value != null
                ? m_Value.GetEditorDescription()
                : "[ No value ]";
        }

        private void lklText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    // If there is no value, open the palette
                    if (Value == null)
                    {
                        // Show palette
                        using (var palette_form = new FormVisualScriptValuePalette())
                        {
                            if (palette_form.ShowDialog() == DialogResult.OK)
                            {
                                // If the user picked a new node from the palette, apply it now
                                Value = palette_form.NewNode;
                                UpdateValueDisplay();
                            }
                        }
                        break;
                    }

                    // Otherwise, open editor form for this node
                    using (FormScriptNode editor_form = ScriptNodeFormMapper.CreateEditorForm(Value))
                    {
                        // If this node cannot be edited, there's nothing more to do
                        if (editor_form == null)
                            return;

                        // Display the node edit dialog
                        if (editor_form.ShowDialog(this) == DialogResult.OK)
                            UpdateValueDisplay();
                    }

                    break;

                case MouseButtons.Right:
                    // Erase the linked value tree
                    Value = null;
                    break;
            }
        }

    }

}
