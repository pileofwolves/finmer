/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Linq;
using System.Text;
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
            chkVoreAlwaysSwallow.Enabled = mode;
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

    }

}
