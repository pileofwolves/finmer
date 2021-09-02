/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    public partial class FormDocumentItem : AssetWindow
    {

        private byte[] m_IconBytes;

        public FormDocumentItem()
        {
            InitializeComponent();
        }

        private void FormDocumentItem_Load(object sender, EventArgs e)
        {
            Debug.Assert(Asset != null);

            AssetItem item = (AssetItem)Asset;

            // Core stats
            txtName.Text = item.ObjectName;
            txtAlias.Text = item.ObjectAlias;
            txtFlavor.Text = item.FlavorText;
            txtGuid.Text = Asset.ID.ToString();
            cmbType.SelectedIndex = (int)item.ItemType;
            nudValue.Value = item.PurchaseValue;
            chkQuest.Checked = item.IsQuestItem;

            // Usable item data
            txtUseDesc.Text = item.UseDescription;
            chkItemConsumable.Checked = item.IsConsumable;
            chkUseField.Checked = item.CanUseInField;
            chkUseBattle.Checked = item.CanUseInBattle;

            // Item icon
            UpdateIcon(item.InventoryIcon);

            // Ensure the asset is marked as dirty if a control on the form is changed
            MakeControlsDirty(this);
        }

        public override void Flush()
        {
            base.Flush();

            AssetItem item = (AssetItem)Asset;

            // Core stats
            item.ObjectName = txtName.Text;
            item.ObjectAlias = txtAlias.Text;
            item.FlavorText = txtFlavor.Text;
            item.ItemType = (AssetItem.EItemType)cmbType.SelectedIndex;
            item.PurchaseValue = (int)nudValue.Value;
            item.IsQuestItem = chkQuest.Checked;

            // Usable item data
            item.UseDescription = txtUseDesc.Text;
            item.IsConsumable = chkItemConsumable.Checked;
            item.CanUseInField = chkUseField.Checked;
            item.CanUseInBattle = chkUseBattle.Checked;

            // Inventory icon
            item.InventoryIcon = m_IconBytes;
        }

        private void cmdEditUseScript_Click(object sender, EventArgs e)
        {
            var item = (AssetItem)Asset;
            item.UseScript.Name = item.Name + "_UseScript";

            Program.MainForm.OpenAssetEditor(item.UseScript);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AssetItem item = (AssetItem)Asset;

            // Show/hide the appropriate UI bits
            fraWeapon.Visible = cmbType.SelectedIndex == (int)AssetItem.EItemType.Equipable;
            fraUsable.Visible = cmbType.SelectedIndex == (int)AssetItem.EItemType.Usable;

            // If the item is now Usable, but its UseScript was optimized away, restore it now
            if (cmbType.SelectedIndex == (int)AssetItem.EItemType.Usable && item.UseScript == null)
            {
                item.UseScript = new AssetScript
                {
                    ID = Guid.NewGuid(),
                    Name = item.Name + "_UseScript"
                };
            }
        }

        private void UpdateIcon(byte[] newImage)
        {
            // Try to apply the specified image to the button, for display purposes
            if (newImage != null)
            {
                try
                {
                    var pic = (Bitmap)new ImageConverter().ConvertFrom(newImage);
                    Debug.Assert(pic != null); // should've thrown an exception otherwise

                    if (pic.Width != 32 || pic.Height != 32)
                        throw new InvalidDataException("The image must be 32 x 32 pixels in size.");

                    cmdIcon.Image = pic;
                    m_IconBytes = newImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The image cannot be loaded: " + ex, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            // Image editing controls are only available if the image exists
            cmdIconClear.Enabled = m_IconBytes != null;
            cmdIconExport.Enabled = m_IconBytes != null;
        }

        private void cmdIcon_Click(object sender, EventArgs e)
        {
            // Have the user pick a new icon file
            dlgIconOpen.Title = "Pick Icon for " + Asset.Name;
            if (dlgIconOpen.ShowDialog() != DialogResult.OK)
                return;

            // Apply the new image file
            try
            {
                UpdateIcon(File.ReadAllBytes(dlgIconOpen.FileName));
                Dirty = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Couldn't open the image: " + ex, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdIconClear_Click(object sender, EventArgs e)
        {
            cmdIcon.Image = null;
            m_IconBytes = null;
            cmdIconClear.Enabled = false;
            cmdIconExport.Enabled = false;
            Dirty = true;
        }

    }

}
