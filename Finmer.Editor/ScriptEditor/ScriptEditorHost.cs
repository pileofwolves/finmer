/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Host control that displays the appropriate subtype of script editor for a particular script asset.
    /// </summary>
    public partial class ScriptEditorHost : UserControl
    {

        private ScriptDataWrapper m_Wrapper;
        private UserControl m_ClientEditor;

        public event EventHandler Dirty;

        /// <summary>
        /// Configures which script subtypes the user is allowed to transform the contained script into.
        /// </summary>
        [Browsable(true)]
        public bool AllowInlineScript { get; set; } = true;

        /// <summary>
        /// Configures which script subtypes the user is allowed to transform the contained script into.
        /// </summary>
        [Browsable(true)]
        public bool AllowExternalScript { get; set; } = false;

        /// <summary>
        /// Configures which script subtypes the user is allowed to transform the contained script into.
        /// </summary>
        [Browsable(true)]
        public bool AllowVisualScript { get; set; } = false;

        public ScriptEditorHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display a script object in this host.
        /// </summary>
        public void SetScript(ScriptDataWrapper wrapper)
        {
            m_Wrapper = wrapper;
            PrepareClientEditor();
        }

        /// <summary>
        /// Notify the parent control that the contained script has been modified.
        /// </summary>
        public void MarkDirty()
        {
            Dirty?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Instruct the hosted script editor to flush any pending changes.
        /// </summary>
        public void Flush()
        {
            ((IScriptEditor)m_ClientEditor)?.Flush();
        }

        public void ConvertToInline()
        {
            Debug.Assert(AllowInlineScript);
            Debug.Assert(!(m_Wrapper.Wrapped is ScriptDataInline));

            if (!ConfirmConvertWarning())
                return;

            // Instantiate a new script that copies the target script's content
            var replacement = new ScriptDataInline
            {
                Name = m_Wrapper.Name,
                ScriptText = m_Wrapper.GetScriptText()
            };

            m_Wrapper.Wrapped = replacement;
            PrepareClientEditor();
        }

        public void ConvertToExternal()
        {
            Debug.Assert(AllowExternalScript);
            Debug.Assert(!(m_Wrapper.Wrapped is ScriptDataExternal));

            if (!ConfirmConvertWarning())
                return;

            // Instantiate a new script that copies the target script's content
            var replacement = new ScriptDataExternal
            {
                Name = m_Wrapper.Name,
                ScriptText = m_Wrapper.GetScriptText()
            };

            m_Wrapper.Wrapped = replacement;
            PrepareClientEditor();
        }

        public void ConvertToVisual()
        {
            Debug.Assert(AllowVisualScript);
            Debug.Assert(!(m_Wrapper.Wrapped is ScriptDataVisual));

            if (!ConfirmConvertWarning())
                return;

            // Instantiate an empty visual script
            // (We currently have no reasonable way of converting a Lua script to its node graph equivalent)
            var replacement = new ScriptDataVisual
            {
                Name = m_Wrapper.Name
            };

            m_Wrapper.Wrapped = replacement;
            PrepareClientEditor();
        }

        private void PrepareClientEditor()
        {
            SuspendLayout();

            // Remove any previous editor object
            if (m_ClientEditor != null)
                Controls.Remove(m_ClientEditor);

            // Instantiate a new one
            switch (m_Wrapper.Wrapped)
            {
                case null:
                    m_ClientEditor = new NullScriptEditor(this);
                    break;

                case ScriptDataInline wrapped:
                    m_ClientEditor = new RawScriptEditor(this, wrapped);
                    break;

                case ScriptDataVisual wrapped:
                    m_ClientEditor = new VisualScriptEditor(this, wrapped);
                    break;

                default:
                    throw new InvalidOperationException("Target script type is not supported");
            }

            // Adjust its layout on the page
            Controls.Add(m_ClientEditor);
            m_ClientEditor.Dock = DockStyle.Fill;

            ResumeLayout();
        }

        private bool ConfirmConvertWarning()
        {
            // If there is no script to begin with, then there is no data to convert
            if (m_Wrapper.Wrapped == null)
                return true;

            // Otherwise, show a data loss warning
            var dr = MessageBox.Show("Converting the script to a different type is a destructive operation and cannot be undone. Are you sure you wish to proceed?", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return dr == DialogResult.Yes;
        }

    }

}
