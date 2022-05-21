/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that assigns the player species nouns and adjectives.
    /// </summary>
    public sealed class CommandPlayerSetSpecies : ScriptCommand
    {

        /// <summary>
        /// Singular species noun.
        /// </summary>
        public string Singular { get; set; } = String.Empty;

        /// <summary>
        /// Plural species noun.
        /// </summary>
        public string Plural { get; set; } = String.Empty;

        /// <summary>
        /// Coat descriptive noun.
        /// </summary>
        public string CoatNoun { get; set; } = String.Empty;

        /// <summary>
        /// Coat descriptive adjective.
        /// </summary>
        public string CoatAdjective { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return String.Format(CultureInfo.InvariantCulture, "Change Player Species to '{0}'", Singular);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.Species = \"{0}\"", CoreUtility.EscapeLuaString(Singular));
            output.AppendLine();
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.SpeciesPlural = \"{0}\"", CoreUtility.EscapeLuaString(Plural));
            output.AppendLine();
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.CoatNoun = \"{0}\"", CoreUtility.EscapeLuaString(CoatNoun));
            output.AppendLine();
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.CoatAdjective = \"{0}\"", CoreUtility.EscapeLuaString(CoatAdjective));
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(Singular), Singular.ToLowerInvariant());
            outstream.WriteStringProperty(nameof(Plural), Plural.ToLowerInvariant());
            outstream.WriteStringProperty(nameof(CoatNoun), CoatNoun.ToLowerInvariant());
            outstream.WriteStringProperty(nameof(CoatAdjective), CoatAdjective.ToLowerInvariant());
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Singular = instream.ReadStringProperty(nameof(Singular));
            Plural = instream.ReadStringProperty(nameof(Plural));
            CoatNoun = instream.ReadStringProperty(nameof(CoatNoun));
            CoatAdjective = instream.ReadStringProperty(nameof(CoatAdjective));
        }

    }

}
