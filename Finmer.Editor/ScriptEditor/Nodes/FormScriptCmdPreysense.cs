/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandPreysense.
    /// </summary>
    public partial class FormScriptCmdPreysense : FormScriptNode
    {

        public FormScriptCmdPreysense()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPreysense_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPreysense)Node;
            apcCreature.SelectedGuid = node.CreatureGuid;
            chkTypeOV.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.OralVore);
            chkTypeAV.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.AnalVore);
            chkTypeCV.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.CockVore);
            chkTypeUB.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.Unbirth);
            chkTypeEndo.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.Endo);
            chkTypeEndoOrFatal.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.EndoOrFatal);
            chkTypeDigestReform.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.DigestionReform);
            chkTypeDigestFatal.Checked = node.Mode.HasFlag(CommandPreysense.ESenseType.DigestionFatal);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPreysense)Node;
            node.CreatureGuid = apcCreature.SelectedGuid;
            node.Mode = CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeOV.Checked ? CommandPreysense.ESenseType.OralVore : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeAV.Checked ? CommandPreysense.ESenseType.AnalVore : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeCV.Checked ? CommandPreysense.ESenseType.CockVore : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeUB.Checked ? CommandPreysense.ESenseType.Unbirth : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeEndo.Checked ? CommandPreysense.ESenseType.Endo : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeEndoOrFatal.Checked ? CommandPreysense.ESenseType.EndoOrFatal : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeDigestReform.Checked ? CommandPreysense.ESenseType.DigestionReform : CommandPreysense.ESenseType.None;
            node.Mode |= chkTypeDigestFatal.Checked ? CommandPreysense.ESenseType.DigestionFatal : CommandPreysense.ESenseType.None;
        }

    }

}
