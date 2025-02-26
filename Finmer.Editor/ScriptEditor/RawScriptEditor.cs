/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    public partial class RawScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataInline m_ScriptData;

        public RawScriptEditor(ScriptEditorHost host, ScriptDataInline script)
        {
            InitializeComponent();

            m_Host = host;
            m_ScriptData = script;

            // Initialize the editor. Note: This MUST be done in the constructor, instead of in the Load handler, because
            // Load will only be raised when the control actually becomes visible, and this may not happen if this control
            // is used in a tabbed/tiered context and the user may or may not ever open these tabs.
            ScintillaHelper.Setup(scintilla, ScintillaHelper.EScintillaStyle.Lua);
            scintilla.Text = m_ScriptData.ScriptText;
            scintilla.EmptyUndoBuffer();
        }

        public void Flush()
        {
            m_ScriptData.ScriptText = scintilla.Text;
        }

        private void RawScriptEditor_Load(object sender, EventArgs e)
        {
            // Hook up the change event now that programmatic text changes are finished
            scintilla.TextChanged += scintilla_TextChanged;
            UpdateToolbarButtons();
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            m_Host.MarkDirty();
            UpdateToolbarButtons();
        }

        private void UpdateToolbarButtons()
        {
            tsbUndo.Enabled = scintilla.CanUndo;
            tsbRedo.Enabled = scintilla.CanRedo;
            tsbLoadOrder.Visible = !m_Host.AllowInlineScript && !m_Host.AllowVisualActionScript && !m_Host.AllowVisualConditionScript;
            tsbConvertVisual.Visible = m_Host.AllowVisualActionScript || m_Host.AllowVisualConditionScript;
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            if (!scintilla.CanUndo)
                return;

            scintilla.Undo();
            UpdateToolbarButtons();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            if (!scintilla.CanRedo)
                return;

            scintilla.Redo();
            UpdateToolbarButtons();
        }

        private void tsbConvertVisual_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToVisual();
        }

        private void tsbLoadOrder_Click(object sender, EventArgs e)
        {
            m_Host.OpenLoadOrderEditor();
        }

    }

}
