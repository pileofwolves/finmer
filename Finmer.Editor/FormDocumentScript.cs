/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    public partial class FormDocumentScript : AssetWindow
    {

        private bool m_Ready;

        public FormDocumentScript()
        {
            InitializeComponent();
        }

        private void FormDocumentScript_Load(object sender, EventArgs e)
        {
            ScintillaHelper.Setup(scintilla);

            var script = (AssetScript)Asset;
            scintilla.Text = script.ScriptText;
            scintilla.EmptyUndoBuffer();
            m_Ready = true;
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            // Avoid marking the asset as dirty if we're still initializing
            if (!m_Ready)
                return;

            Dirty = true;
        }

        public override void Flush()
        {
            base.Flush();

            var script = (AssetScript)Asset;
            script.ScriptText = scintilla.Text;
        }

    }

}
