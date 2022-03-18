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
using WeifenLuo.WinFormsUI.Docking;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a dockable window that allows editing of a content object.
    /// </summary>
    public class EditorWindow : DockContent
    {

        /// <summary>
        /// Gets or sets whether the asset has been modified since the last time it was flushed.
        /// </summary>
        public bool Dirty
        {
            get => m_Dirty;
            set
            {
                m_Dirty = value;
                if (value)
                    Program.MainForm.MarkDirty();
                UpdateText();
            }
        }

        private bool m_Dirty;

        protected EditorWindow()
        {
            Load += EditorWindow_Load;
            FormClosing += EditorWindow_FormClosing;
        }

        /// <summary>
        /// Requests that the asset editor copies its local changes back to the <seealso cref="AssetBase" /> object.
        /// </summary>
        public virtual void Flush()
        {
            Dirty = false;
        }

        /// <summary>
        /// Updates the window title to include the asset name and a dirty indicator.
        /// </summary>
        public void UpdateText()
        {
            if (Dirty)
                Text = GetWindowTitle() + "*";
            else
                Text = GetWindowTitle();
        }

        private void EditorWindow_Load(object sender, EventArgs args)
        {
            UpdateText();
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs args)
        {
            if (Dirty)
            {
                DialogResult dr = MessageBox.Show($"Would you like to save changes to {GetWindowTitle()}?", "Finmer Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                    args.Cancel = true;
                if (dr == DialogResult.Yes)
                    Flush();
            }
        }

        /// <summary>
        /// Adds an event hook to a Control and its children, so that they mark the Asset as dirty if the control is changed.
        /// </summary>
        /// <param name="ctl">The control (and its children) to hook.</param>
        protected void MakeControlsDirty(Control ctl)
        {
            // Hook changed events so that the asset is marked dirty when the control is changed by the user
            switch (ctl)
            {
                case TextBox _:
                    ctl.TextChanged += (sender, args) => Dirty = true;
                    break;

                case NumericUpDown nud:
                    nud.ValueChanged += (sender, args) => Dirty = true;
                    nud.TextChanged += (sender, args) => Dirty = true;
                    break;

                case CheckBox chk:
                    chk.CheckedChanged += (sender, args) => Dirty = true;
                    break;

                case ComboBox cmb:
                    cmb.TextChanged += (sender, args) => Dirty = true;
                    cmb.SelectedIndexChanged += (sender, args) => Dirty = true;
                    break;

                case AssetPickerControl picker:
                    picker.SelectedAssetChanged += (sender, args) => Dirty = true;
                    break;
            }

            // Recursively run this on child controls too
            foreach (Control child in ctl.Controls)
                MakeControlsDirty(child);
        }

        /// <summary>
        /// Returns the name of the object being edited.
        /// </summary>
        protected virtual string GetWindowTitle()
        {
            return "Window";
        }

    }

}
