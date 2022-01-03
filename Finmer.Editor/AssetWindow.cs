/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.Assets;
using WeifenLuo.WinFormsUI.Docking;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a dockable window that is responsible for editing one asset.
    /// </summary>
    public class AssetWindow : DockContent
    {

        private bool m_Dirty;

        /// <summary>
        /// Constructs a new AssetWindow.
        /// </summary>
        protected AssetWindow()
        {
            Load += (sender, args) =>
            {
                if (Asset != null)
                    Text = Asset.Name;
            };
            FormClosing += (sender, args) =>
            {
                if (Dirty)
                {
                    DialogResult dr = MessageBox.Show($"Would you like to save changes to {Asset?.Name ?? "this asset"}?",
                        "Finmer Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (dr == DialogResult.Cancel) args.Cancel = true;
                    if (dr == DialogResult.Yes) Flush();
                }
            };
        }

        /// <summary>
        /// Gets or sets the asset that this window is editing.
        /// </summary>
        public AssetBase Asset { get; set; }

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
            if (Asset == null) return;

            if (Dirty)
                Text = Asset.Name + "*";
            else
                Text = Asset.Name;
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

    }

}
