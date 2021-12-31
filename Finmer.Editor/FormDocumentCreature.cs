/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Buffs;

namespace Finmer.Editor
{

    public partial class FormDocumentCreature : AssetWindow
    {

        public FormDocumentCreature()
        {
            InitializeComponent();
        }

        private void FormDocumentCreature_Load(object sender, EventArgs e)
        {
            AssetCreature creature = (AssetCreature)Asset;

            // Core stats
            txtName.Text = creature.ObjectName;
            txtAlias.Text = creature.ObjectAlias;
            txtGuid.Text = Asset.ID.ToString();
            nudStr.Value = creature.Strength;
            nudDex.Value = creature.Agility;
            nudCon.Value = creature.Body;
            nudWis.Value = creature.Wits;
            nudLevel.Value = creature.Level;
            cmbGender.SelectedIndex = (int)creature.Gender;
            cmbSize.SelectedIndex = (int)creature.Size;

            // Vore stats
            chkVorePred.Checked = creature.PredatorEnabled;
            chkVoreDigest.Checked = creature.PredatorDigests;
            chkVoreDisposal.Checked = creature.PredatorDisposal;
            chkAutoVorePred.Checked = creature.AutoSwallowPlayer;
            chkAutoVorePrey.Checked = creature.AutoSwallowedByPlayer;
            chkVorePred_CheckedChanged(sender, e); // Make sure to update enabled states

            // Combat flags
            var flags = (ECharacterFlags)creature.Flags;
            chkFlagNoGrapple.Checked = (flags & ECharacterFlags.NoGrapple) > 0;
            chkFlagNoVore.Checked = (flags & ECharacterFlags.NoPrey) > 0;
            chkFlagNoFight.Checked = (flags & ECharacterFlags.NoFight) > 0;
            chkFlagNoXP.Checked = (flags & ECharacterFlags.NoXP) > 0;
            chkFlagSkipTurns.Checked = (flags & ECharacterFlags.SkipTurns) > 0;

            // Equipment
            assetEquip1.SelectedGuid = creature.Equipment[0];
            assetEquip2.SelectedGuid = creature.Equipment[1];
            assetEquip3.SelectedGuid = creature.Equipment[2];
            assetEquip4.SelectedGuid = creature.Equipment[3];

            // String mappings
            foreach (var mapping in creature.StringMappings)
            {
                // Add a list item to the UI for each registered string mapping
                ListViewItem item = new ListViewItem();
                PopulateStringMappingView(item, mapping);
                lsvStringMappings.Items.Add(item);
            }

            // Mark the asset as dirty if any control on the form is changed
            MakeControlsDirty(this);

            // Populate the stats overview with initial data
            UpdateCombatOverview();
        }

        private void chkVorePred_CheckedChanged(object sender, EventArgs e)
        {
            bool mode = chkVorePred.Checked;
            chkVoreDigest.Enabled = mode;
            chkVoreDisposal.Enabled = mode;
            chkAutoVorePred.Enabled = mode;
        }

        public override void Flush()
        {
            base.Flush();

            AssetCreature creature = (AssetCreature)Asset;

            // Core stats
            creature.ObjectName = txtName.Text;
            creature.ObjectAlias = txtAlias.Text;
            creature.Strength = (int)nudStr.Value;
            creature.Agility = (int)nudDex.Value;
            creature.Body = (int)nudCon.Value;
            creature.Wits = (int)nudWis.Value;
            creature.Level = (int)nudLevel.Value;
            creature.Gender = (EGender)cmbGender.SelectedIndex;
            creature.Size = (AssetCreature.ESize)cmbSize.SelectedIndex;

            // Vore stats
            creature.PredatorEnabled = chkVorePred.Checked;
            creature.PredatorDigests = chkVoreDigest.Checked;
            creature.PredatorDisposal = chkVoreDisposal.Checked;
            creature.AutoSwallowPlayer = chkAutoVorePred.Checked;
            creature.AutoSwallowedByPlayer = chkAutoVorePrey.Checked;

            // Combat flags
            var flags = ECharacterFlags.None;
            flags |= chkFlagNoGrapple.Checked ? ECharacterFlags.NoGrapple : 0;
            flags |= chkFlagNoVore.Checked ? ECharacterFlags.NoPrey : 0;
            flags |= chkFlagNoFight.Checked ? ECharacterFlags.NoFight : 0;
            flags |= chkFlagNoXP.Checked ? ECharacterFlags.NoXP : 0;
            flags |= chkFlagSkipTurns.Checked ? ECharacterFlags.SkipTurns : 0;
            creature.Flags = (int)flags;

            // Equipment
            creature.Equipment[0] = assetEquip1.SelectedGuid;
            creature.Equipment[1] = assetEquip2.SelectedGuid;
            creature.Equipment[2] = assetEquip3.SelectedGuid;
            creature.Equipment[3] = assetEquip4.SelectedGuid;

            // String mappings
            creature.StringMappings.Clear();
            foreach (ListViewItem item in lsvStringMappings.Items)
            {
                var mapping = (StringMapping)item.Tag;
                creature.StringMappings.Add(mapping);
            }

            // Update the stats overview
            UpdateCombatOverview();
        }

        private void UpdateCombatOverview()
        {
            StringBuilder builder = new StringBuilder();
            AssetCreature creature = (AssetCreature)Asset;

            // Gather all equipment items
            var all_equipment = new[]
            {
                assetEquip1.SelectedAsset,
                assetEquip2.SelectedAsset,
                assetEquip3.SelectedAsset,
                assetEquip4.SelectedAsset,
            };

            // Gather all buffs this creature has on its equipment
            var all_buffs = all_equipment
                .Where(item => item != null)
                .OfType<AssetItem>()
                .SelectMany(item => item.EquipEffects)
                .ToList();

            // Attack dice
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:+#;-#;0}",
                Math.Max(1, creature.Strength + all_buffs.OfType<BuffAttackDice>().Sum(buff => buff.Delta)));
            builder.AppendLine();

            // Defense dice
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:+#;-#;0}",
                Math.Max(1, creature.Strength + all_buffs.OfType<BuffDefenseDice>().Sum(buff => buff.Delta)));
            builder.AppendLine();

            // Grapple dice
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:+#;-#;0}",
                Math.Max(1, creature.Agility));
            builder.AppendLine();

            // Swallow dice
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:+#;-#;0}",
                Math.Max(1, creature.Strength));
            builder.AppendLine();

            // Struggle dice
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:+#;-#;0}",
                Math.Max(1, creature.Agility));

            lblCombatOverview.Text = builder.ToString();
        }

        /// <summary>
        /// Returns a human-readable string that describes a particular StringMapping.
        /// </summary>
        private static void PopulateStringMappingView(ListViewItem item, StringMapping mapping)
        {
            // Ensure there are three sub-items to work with. (Note that the main column counts as a sub-item as well, hence three.)
            if (item.SubItems.Count < 3)
            {
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
            }

            // Update the text of all displays
            item.SubItems[0].Text = mapping.Key;
            item.SubItems[1].Text = mapping.Rule.ToString();
            item.SubItems[2].Text = mapping.NewKey;

            // Cache the mapping itself in the item so it can be retrieved by Flush()
            item.Tag = mapping;
        }

        private void cmdStringMappingAdd_Click(object sender, EventArgs e)
        {
            // Add a blank string mapping to the list
            ListViewItem item = new ListViewItem();
            PopulateStringMappingView(item, new StringMapping());
            lsvStringMappings.Items.Add(item);

            // Mark the module as changed
            Dirty = true;

            // Select this item
            item.Selected = true;

            // Open the edit dialog, since the user will likely want to populate the blank mapping
            Debug.Assert(lsvStringMappings.SelectedItems.Count == 1);
            cmdStringMappingEdit_Click(sender, e);
        }

        private void cmdStringMappingEdit_Click(object sender, EventArgs e)
        {
            // Open the edit dialog for the selected item
            var selected_item = lsvStringMappings.SelectedItems[0];
            var mapping = (StringMapping)selected_item.Tag;

            using (var dialog = new FormStringMappingEdit(mapping))
            {
                // Present the dialog
                if (dialog.ShowDialog() == DialogResult.Cancel)
                    return;

                // If the user accepted the changes, copy them over
                PopulateStringMappingView(selected_item, dialog.Mapping);

                // Mark the module as changed
                Dirty = true;
            }
        }

        private void cmdStringMappingRemove_Click(object sender, EventArgs e)
        {
            // Delete the selected items
            int selected_index = lsvStringMappings.SelectedIndices[0];
            lsvStringMappings.Items.RemoveAt(selected_index);

            // Mark the module as changed
            Dirty = true;
        }

        private void lsvStringMappings_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Edit controls are enabled if there is a selection to edit
            bool enabled = lsvStringMappings.SelectedItems.Count == 1;
            cmdStringMappingEdit.Enabled = enabled;
            cmdStringMappingRemove.Enabled = enabled;
        }

        private void lsvStringMappings_DoubleClick(object sender, EventArgs e)
        {
            // Treat a double-click as equivalent to clicking the Edit button
            if (lsvStringMappings.SelectedItems.Count == 1)
                cmdStringMappingEdit_Click(sender, e);
        }

    }

}
