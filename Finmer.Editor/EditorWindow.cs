/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<WeakReference<EditorWindow>> m_NestedWindows = new List<WeakReference<EditorWindow>>();

        protected EditorWindow()
        {
            Load += EditorWindow_Load;
            FormClosing += EditorWindow_FormClosing;
            FormClosed += EditorWindow_FormClosed;
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

        /// <summary>
        /// Sets this window as owning the specified window, so that it will be flushed and closed together with this one.
        /// </summary>
        protected void RegisterNestedWindow(EditorWindow nested)
        {
            // Check if this exact asset is already owned, so we don't register a duplicate window
            foreach (var reference in m_NestedWindows)
                if (reference.TryGetTarget(out var candidate) && candidate == nested)
                    return;

            // These are weak references so that this parent window doesn't keep the nested editors alive after the user closes their tabs
            m_NestedWindows.Add(new WeakReference<EditorWindow>(nested));
        }

        private void EditorWindow_Load(object sender, EventArgs args)
        {
            UpdateText();
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs args)
        {
            // We should display the save prompt if either the main window, or any nested window, is dirty
            bool any_dirty = Dirty || m_NestedWindows.Any(reference => reference.TryGetTarget(out var window) && !window.IsDisposed && window.Dirty);

            // Save confirmation prompt
            if (any_dirty)
            {
                DialogResult dr = MessageBox.Show($"Would you like to save changes to {GetWindowTitle()}?", "Finmer Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                switch (dr)
                {
                    case DialogResult.Cancel:
                        args.Cancel = true;
                        break;

                    case DialogResult.Yes:
                        // Flush all nested windows
                        foreach (var reference in m_NestedWindows)
                            if (reference.TryGetTarget(out var nested_window) && !nested_window.IsDisposed)
                                nested_window.Flush(); 
                        
                        // Flush the main asset
                        Flush();
                        break;
                }
            }
        }

        private void EditorWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var reference in m_NestedWindows)
            {
                if (reference.TryGetTarget(out var nested_window) && !nested_window.IsDisposed)
                {
                    // Force the window to close, without displaying a confirmation prompt
                    nested_window.Dirty = false;
                    nested_window.Close();
                }
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
