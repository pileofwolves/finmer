/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents an interface that allows the user to instantiate one of the allowed script subtypes.
    /// </summary>
    public partial class NullScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;

        public NullScriptEditor(ScriptEditorHost host)
        {
            InitializeComponent();

            m_Host = host;
            cmdCreateVisual.Enabled = host.AllowVisualActionScript || host.AllowVisualConditionScript;
            cmdCreateRaw.Enabled = host.AllowInlineScript || host.AllowExternalScript;
        }

        public void Flush()
        {
            // No data to flush
        }

        private void cmdCreateVisual_Click(object sender, System.EventArgs e)
        {
            // Create a visual script in place of this null placeholder
            m_Host.ConvertToVisual();
        }

        private void cmdCreateRaw_Click(object sender, System.EventArgs e)
        {
            // Try either of the raw formats
            if (m_Host.AllowExternalScript)
                m_Host.ConvertToExternal();
            else
                m_Host.ConvertToInline();
        }

        private void NullScriptEditor_MouseLeave(object sender, System.EventArgs e)
        {
            // If the cursor is still inside the script editor control, don't hide the popup menu
            if (ClientRectangle.Contains(PointToClient(Cursor.Position)))
                return;

            lblEmptyNotice.Visible = true;
            groupBox.Visible = false;
        }

        private void lblEmptyNotice_MouseEnter(object sender, System.EventArgs e)
        {
            // Replace empty notice with a popup-like menu
            // This is done to reduce visual clutter; the script creation menu is quite busy on the eye.
            lblEmptyNotice.Visible = false;
            groupBox.Visible = true;
        }

    }

}
