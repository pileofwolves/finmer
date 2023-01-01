/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandSetScene.
    /// </summary>
    public partial class FormScriptCmdSetScene : FormScriptNode
    {

        public FormScriptCmdSetScene()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, System.EventArgs e)
        {
            var node = (CommandSetScene)Node;
            apcScene.SelectedGuid = node.SceneGuid;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandSetScene)Node;
            node.SceneGuid = apcScene.SelectedGuid;
            node.SceneName = apcScene.SelectedAsset?.Name ?? "[unknown]";
        }

    }

}
