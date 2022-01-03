/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
using Finmer.Core.Buffs;

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

            // Equipment data
            cmbEquipSlot.SelectedIndex = (int)item.EquipSlot;
            foreach (var buff in item.EquipEffects)
                AddEquipEffect(buff, false);

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

            // Equipment data
            item.EquipSlot = (AssetItem.EEquipSlot)cmbEquipSlot.SelectedIndex;
            item.EquipEffects.Clear();
            foreach (var buff_item in lsvEquipEffects.Items)
            {
                var buff = (Buff)((ListViewItem)buff_item).Tag;
                item.EquipEffects.Add(buff);
            }

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
            fraEquipment.Visible = cmbType.SelectedIndex == (int)AssetItem.EItemType.Equipable;
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

        private void AddEquipEffect(Buff buff, bool isUserAction = true)
        {
            // Create a new list entry that represents this effect
            var buff_item = new ListViewItem();
            buff_item.SubItems.Add(buff.GetDescription());
            buff_item.Text = buff.GetIcon().ToString();
            buff_item.Tag = buff;
            lsvEquipEffects.Items.Add(buff_item);

            // Mark asset as having changed
            Dirty |= isUserAction;
        }

        private static BaseEffectEditor CreateEquipEffectEditor(Buff buff)
        {
            switch (buff)
            {
                case SingleDeltaBuff _:
                    return new FormEffectEditorSingleDelta();

                default:
                    throw new ArgumentException(nameof(buff));
            }
        }

        private void EditSelectedEquipEffect()
        {
            // Validate that there actually is a selection to edit
            if (lsvEquipEffects.SelectedItems.Count == 0)
                return;

            // Get the Buff object associated with the selected row
            var item = lsvEquipEffects.SelectedItems[0];
            var buff = (Buff)item.Tag;

            using (var form = CreateEquipEffectEditor(buff))
            {
                form.SourceBuff = buff;

                // Save result only if user clicked OK
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                // Make a copy of the buff and store it in the list item
                var new_buff = form.CopyBuff();
                item.SubItems.Clear();
                item.SubItems.Add(new_buff.GetDescription());
                item.Text = new_buff.GetIcon().ToString();
                item.Tag = new_buff;

                // Mark the asset as having changed
                Dirty = true;
            }
        }

        private void cmdEquipEffectAdd_Click(object sender, EventArgs e)
        {
            mnuEquipEffectAdd.Show(cmdEquipEffectAdd, new Point(0, cmdEquipEffectAdd.Height));
        }

        private void cmdEquipEffectRemove_Click(object sender, EventArgs e)
        {
            foreach (var selection in lsvEquipEffects.SelectedItems)
                lsvEquipEffects.Items.Remove((ListViewItem)selection);

            // Mark the asset as having changed
            Dirty = true;
        }

        private void cmdEquipEffectEdit_Click(object sender, EventArgs e)
        {
            EditSelectedEquipEffect();
        }

        private void lsvEquipEffects_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedEquipEffect();
        }

        private void lsvEquipEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool has_selection = lsvEquipEffects.SelectedItems.Count != 0;
            cmdEquipEffectEdit.Enabled = has_selection;
            cmdEquipEffectRemove.Enabled = has_selection;
        }

        private void mnuEffectDiceAttack_Click(object sender, EventArgs e)
        {
            AddEquipEffect(new BuffAttackDice());
        }

        private void mnuEffectDiceDefense_Click(object sender, EventArgs e)
        {
            AddEquipEffect(new BuffDefenseDice());
        }

        private void mnuEffectStatHP_Click(object sender, EventArgs e)
        {
            AddEquipEffect(new BuffHealth());
        }

    }

}
