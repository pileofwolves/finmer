/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents an editor interface for a ScriptDataVisualAction object.
    /// </summary>
    public partial class VisualConditionScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataVisualCondition m_ScriptData;

        public VisualConditionScriptEditor(ScriptEditorHost host, ScriptDataVisualCondition script)
        {
            InitializeComponent();

            m_Host = host;
            m_ScriptData = script;

            // Populate the condition list
            cgeBranch.SetGroup(m_ScriptData.Condition);
        }

        public void Flush()
        {
            // Copy the edited group (which is a copy, not the original) back to the container
            m_ScriptData.Condition = cgeBranch.Group;
        }

        private void VisualScriptEditor_Load(object sender, EventArgs e)
        {
            // The host determines which conversions are permissible
            tsbConvertInline.Visible = m_Host.AllowInlineScript;
            tsbConvertExternal.Visible = m_Host.AllowExternalScript;
        }

        private void tsbConvertInline_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToInline();
        }

        private void tsbConvertExternal_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToExternal();
        }

    }

}
