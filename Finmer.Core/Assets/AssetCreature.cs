﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a template used for instantiating a new Character in gameplay code.
    /// </summary>
    public class AssetCreature : AssetBase
    {

        /// <summary>
        /// Describes the relative size factor of a creature.
        /// </summary>
        public enum ESize : byte
        {
            Tiny,
            Small,
            Medium,
            Large,
            Huge
        }

        /// <summary>
        /// The name of the object, used as value for GameObject.Name.
        /// </summary>
        public string ObjectName { get; set; } = String.Empty;

        /// <summary>
        /// The alternate name of the object, used as value for GameObject.Alias.
        /// </summary>
        public string ObjectAlias { get; set; } = String.Empty;

        /// <summary>
        /// The creature's Strength combat stat.
        /// </summary>
        public int Strength { get; set; } = 3;

        /// <summary>
        /// The creature's Agility combat stat.
        /// </summary>
        public int Agility { get; set; } = 3;

        /// <summary>
        /// The creature's Body combat stat.
        /// </summary>
        public int Body { get; set; } = 3;

        /// <summary>
        /// The creature's Wits combat stat.
        /// </summary>
        public int Wits { get; set; } = 3;

        /// <summary>
        /// Bitmask of type ECharacterFlags.
        /// </summary>
        public int Flags { get; set; }

        /// <summary>
        /// The creature's experience level.
        /// </summary>
        public int Level { get; set; } = 1;

        /// <summary>
        /// The creature's size class.
        /// </summary>
        public ESize Size { get; set; } = ESize.Medium;

        /// <summary>
        /// The creature's configured gender.
        /// </summary>
        public EGender Gender { get; set; }

        /// <summary>
        /// Equipment items assigned to the creature.
        /// </summary>
        public Guid[] Equipment { get; } = new Guid[4];

        /// <summary>
        /// Table of customized combat strings.
        /// </summary>
        public List<StringMapping> StringMappings { get; } = new List<StringMapping>();

        /// <summary>
        /// Whether this creature can initiate vore actions.
        /// </summary>
        public bool PredatorEnabled { get; set; }

        /// <summary>
        /// Whether this creature deals digestion damage to swallowed prey.
        /// </summary>
        public bool PredatorDigests { get; set; }

        /// <summary>
        /// Whether this creature has a disposal scene. If set, the game will automatically set it up.
        /// </summary>
        public bool PredatorDisposal { get; set; }

        /// <summary>
        /// Whether this creature will always swallow the player if they kill the player through non-vore combat actions.
        /// </summary>
        public bool AutoSwallowPlayer { get; set; }

        /// <summary>
        /// Whether this creature will always be swallowed by the player if they are killed through non-vore combat actions.
        /// </summary>
        public bool AutoSwallowedByPlayer { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Core stats
            outstream.WriteStringProperty(nameof(ObjectName), ObjectName);
            outstream.WriteStringProperty(nameof(ObjectAlias), ObjectAlias);
            outstream.WriteCompressedInt32Property(nameof(Strength), Strength);
            outstream.WriteCompressedInt32Property(nameof(Agility), Agility);
            outstream.WriteCompressedInt32Property(nameof(Body), Body);
            outstream.WriteCompressedInt32Property(nameof(Wits), Wits);
            outstream.WriteCompressedInt32Property(nameof(Flags), Flags);
            outstream.WriteCompressedInt32Property(nameof(Level), Level);
            outstream.WriteEnumProperty(nameof(Size), Size);
            outstream.WriteEnumProperty(nameof(Gender), Gender);

            // Equipment links
            outstream.WriteGuidProperty("Equipment1", Equipment[0]);
            outstream.WriteGuidProperty("Equipment2", Equipment[1]);
            outstream.WriteGuidProperty("Equipment3", Equipment[2]);
            outstream.WriteGuidProperty("Equipment4", Equipment[3]);

            // Vore stats
            outstream.WriteBooleanProperty("IsPredator", PredatorEnabled);
            outstream.WriteBooleanProperty("AutoVorePrey", AutoSwallowedByPlayer);
            if (PredatorEnabled)
            {
                outstream.WriteBooleanProperty("AutoVorePredator", AutoSwallowPlayer);
                outstream.WriteBooleanProperty("PredatorDigests", PredatorDigests);
                outstream.WriteBooleanProperty("PredatorDisposal", PredatorDisposal);
            }

            // String re-mappings
            outstream.BeginArray("StringMappings", StringMappings.Count);
            foreach (var pair in StringMappings)
            {
                outstream.BeginObject();
                outstream.WriteStringProperty("Key", pair.Key);
                outstream.WriteEnumProperty("Rule", pair.Rule);
                outstream.WriteStringProperty("NewKey", pair.NewKey);
                outstream.EndObject();
            }

            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Core stats
            ObjectName = instream.ReadStringProperty(nameof(ObjectName));
            ObjectAlias = instream.ReadStringProperty(nameof(ObjectAlias));
            Strength = instream.ReadCompressedInt32Property(nameof(Strength));
            Agility = instream.ReadCompressedInt32Property(nameof(Agility));
            Body = instream.ReadCompressedInt32Property(nameof(Body));
            Wits = instream.ReadCompressedInt32Property(nameof(Wits));
            Flags = instream.ReadCompressedInt32Property(nameof(Flags));
            Level = instream.ReadCompressedInt32Property(nameof(Level));
            Size = instream.ReadEnumProperty<ESize>(nameof(Size));
            Gender = instream.ReadEnumProperty<EGender>(nameof(Gender));

            // Equipment links
            Equipment[0] = instream.ReadGuidProperty("Equipment1");
            Equipment[1] = instream.ReadGuidProperty("Equipment2");
            Equipment[2] = instream.ReadGuidProperty("Equipment3");
            Equipment[3] = instream.ReadGuidProperty("Equipment4");

            // Vore stats
            PredatorEnabled = instream.ReadBooleanProperty("IsPredator");
            AutoSwallowedByPlayer = instream.ReadBooleanProperty("AutoVorePrey");
            if (PredatorEnabled)
            {
                AutoSwallowPlayer = instream.ReadBooleanProperty("AutoVorePredator");
                PredatorDigests = instream.ReadBooleanProperty("PredatorDigests");
                PredatorDisposal = instream.ReadBooleanProperty("PredatorDisposal");
            }

            // String re-mappings
            for (int count = instream.BeginArray("StringMappings"); count > 0; count--)
            {
                instream.BeginObject();
                {
                    var key = instream.ReadStringProperty("Key");
                    var rule = instream.ReadEnumProperty<StringMapping.ERule>("Rule");
                    var value = instream.ReadStringProperty("NewKey");
                    StringMappings.Add(new StringMapping
                    {
                        Key = key,
                        Rule = rule,
                        NewKey = value
                    });
                }
                instream.EndObject();
            }
            instream.EndArray();
        }

    }

}
