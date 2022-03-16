/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a Lua script that is built using modular, visually arranged nodes.
    /// </summary>
    public sealed class ScriptDataVisual : ScriptData
    {

        public override string GetScriptText()
        {
            throw new System.NotImplementedException();
        }

        public override bool HasContent()
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            throw new System.NotImplementedException();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            throw new System.NotImplementedException();
        }

    }

}
