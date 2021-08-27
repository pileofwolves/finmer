/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;
using Finmer.Core.Assets;

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

            var flags = (ECharacterFlags)creature.Flags;
            chkFlagNoGrapple.Checked = (flags & ECharacterFlags.NoGrapple) > 0;
            chkFlagNoVore.Checked = (flags & ECharacterFlags.NoPrey) > 0;
            chkFlagNoFight.Checked = (flags & ECharacterFlags.NoFight) > 0;
            chkFlagNoXP.Checked = (flags & ECharacterFlags.NoXP) > 0;
            chkFlagSkipTurns.Checked = (flags & ECharacterFlags.SkipTurns) > 0;
            chkFlagSkipAOO.Checked = (flags & ECharacterFlags.SkipAOO) > 0;
            chkFlagFailGrapple.Checked = (flags & ECharacterFlags.FailGrapple) > 0;
            chkFlagFailVore.Checked = (flags & ECharacterFlags.FailVore) > 0;

            // mark the asset as dirty if a control on the form is changed
            MakeControlsDirty(this);
        }

        private void chkVorePred_CheckedChanged(object sender, EventArgs e)
        {
            bool mode = chkVorePred.Checked;
            chkVoreDigest.Enabled = mode;
            lblVorePredness.Enabled = mode;
            nudVorePredness.Enabled = mode;
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

            var flags = ECharacterFlags.None;
            flags |= chkFlagNoGrapple.Checked ? ECharacterFlags.NoGrapple : 0;
            flags |= chkFlagNoVore.Checked ? ECharacterFlags.NoPrey : 0;
            flags |= chkFlagNoFight.Checked ? ECharacterFlags.NoFight : 0;
            flags |= chkFlagNoXP.Checked ? ECharacterFlags.NoXP : 0;
            flags |= chkFlagSkipTurns.Checked ? ECharacterFlags.SkipTurns : 0;
            flags |= chkFlagSkipAOO.Checked ? ECharacterFlags.SkipAOO : 0;
            flags |= chkFlagFailGrapple.Checked ? ECharacterFlags.FailGrapple : 0;
            flags |= chkFlagFailVore.Checked ? ECharacterFlags.FailVore : 0;
            creature.Flags = (int)flags;
        }

    }

}
