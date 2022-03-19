﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Asset editor window that supports editing of ScriptDataExternal.
    /// </summary>
    public partial class FormDocumentScriptExternal : AssetWindow
    {

        private ScriptDataWrapper m_Wrapper;

        public FormDocumentScriptExternal()
        {
            InitializeComponent();
        }

        private void FormDocumentScriptExternal_Load(object sender, EventArgs e)
        {
            AssetScript script = (AssetScript)Asset;

            // If the script asset doesn't have an assigned script container yet, create one now
            if (script.Contents == null)
                script.Contents = new ScriptDataExternal { Name = Asset.Name };

            // Wrap the script to ensure we can modify its subtype
            m_Wrapper = ScriptDataWrapper.EnsureWrapped(script.Contents);
            script.Contents = m_Wrapper;

            // Configure the editor host
            scriptEditorHost.SetScript(m_Wrapper);
            scriptEditorHost.Dirty += ScriptEditorHost_Dirty;
        }

        private void ScriptEditorHost_Dirty(object sender, EventArgs e)
        {
            // Pass the notification up to the AssetWindow
            Dirty = true;
        }

        public override void Flush()
        {
            base.Flush();

            // Update the script name, in case it was changed
            m_Wrapper.Name = Name;
            m_Wrapper.Wrapped.Name = Name;

            // Flush the script editor
            scriptEditorHost.Flush();
        }

    }

}