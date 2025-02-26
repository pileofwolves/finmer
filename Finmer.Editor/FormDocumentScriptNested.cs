/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Asset editor window that supports editing of ScriptDataExternal and ScriptDataVisual.
    /// </summary>
    public partial class FormDocumentScriptNested : EditorWindow
    {

        public ScriptDataWrapper ScriptWrapper { get; set; }

        public FormDocumentScriptNested()
        {
            InitializeComponent();
        }

        private void FormDocumentScriptInternal_Load(object sender, EventArgs e)
        {
            // Configure the editor host
            scriptEditorHost.SetScript(ScriptWrapper);
            scriptEditorHost.Dirty += ScriptEditorHost_Dirty;
        }

        private void ScriptEditorHost_Dirty(object sender, EventArgs e)
        {
            // Pass the notification up to the AssetWindow
            Dirty = true;
        }

        public override void Flush()
        {
            // Flush the script editor
            scriptEditorHost.Flush();

            base.Flush();
        }

        protected override string GetWindowTitle()
        {
            return ScriptWrapper?.Name ?? String.Empty;
        }

    }

}
