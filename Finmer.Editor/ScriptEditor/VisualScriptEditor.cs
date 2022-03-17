/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
    /// Represents an editor interface for a raw (text-based) Lua script.
    /// </summary>
    public partial class VisualScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataVisual m_ScriptData;

        public VisualScriptEditor(ScriptEditorHost host, ScriptDataVisual script)
        {
            InitializeComponent();

            m_Host = host;
            m_ScriptData = script;
        }

        public void Flush()
        {
            // TODO
        }

        private void VisualScriptEditor_Load(object sender, EventArgs e)
        {
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
