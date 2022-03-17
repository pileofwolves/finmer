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
    public partial class RawScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataInline m_ScriptData;

        public RawScriptEditor(ScriptEditorHost host, ScriptDataInline script)
        {
            InitializeComponent();

            m_Host = host;
            m_ScriptData = script;
        }

        public void Flush()
        {
            m_ScriptData.ScriptText = scintilla.Text;
        }

        private void RawScriptEditor_Load(object sender, EventArgs e)
        {
            // Initialize the editor
            ScintillaHelper.Setup(scintilla, true);
            scintilla.Text = m_ScriptData.ScriptText;
            scintilla.EmptyUndoBuffer();

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
            tsbConvertVisual.Visible = m_Host.AllowVisualScript;
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

    }

}
