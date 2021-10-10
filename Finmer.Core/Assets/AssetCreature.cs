﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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

        public enum ESize
        {
            Tiny,
            Small,
            Medium,
            Large,
            Huge
        }

        public struct StringMapping
        {

            public string Rule { get; set; }

            public string NewValue { get; set; }

        }

        /// <summary>
        /// The name of the object, used as value for GameObject.Name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// The alternate name of the object, used as value for GameObject.Alias.
        /// </summary>
        public string ObjectAlias { get; set; }

        /// <summary>
        /// The creature's Strength combat stat.
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// The creature's Agility combat stat.
        /// </summary>
        public int Agility { get; set; }

        /// <summary>
        /// The creature's Body combat stat.
        /// </summary>
        public int Body { get; set; }

        /// <summary>
        /// The creature's Wits combat stat.
        /// </summary>
        public int Wits { get; set; }

        /// <summary>
        /// Bitflags of type ECharacterFlags.
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
        public Dictionary<string, StringMapping> StringMappings { get; } = new Dictionary<string, StringMapping>();

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
        public bool PredatorAlwaysSwallowPlayer { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Core stats
            outstream.WriteStringProperty("ObjectName", ObjectName);
            outstream.WriteStringProperty("ObjectAlias", ObjectAlias);
            outstream.WriteInt32Property("Strength", Strength);
            outstream.WriteInt32Property("Agility", Agility);
            outstream.WriteInt32Property("Body", Body);
            outstream.WriteInt32Property("Wits", Wits);
            outstream.WriteInt32Property("Flags", Flags);
            outstream.WriteInt32Property("Level", Level);
            outstream.WriteEnumProperty("Size", Size);
            outstream.WriteEnumProperty("Gender", Gender);

            // Equipment links
            outstream.WriteGuidProperty("Equipment1", Equipment[0]);
            outstream.WriteGuidProperty("Equipment2", Equipment[1]);
            outstream.WriteGuidProperty("Equipment3", Equipment[2]);
            outstream.WriteGuidProperty("Equipment4", Equipment[3]);

            // Vore stats
            outstream.WriteBooleanProperty("IsPredator", PredatorEnabled);
            if (PredatorEnabled)
            {
                outstream.WriteBooleanProperty("PredatorDigests", PredatorDigests);
                outstream.WriteBooleanProperty("PredatorDisposal", PredatorDisposal);
                outstream.WriteBooleanProperty("PredatorAlwaysSwallowPlayer", PredatorAlwaysSwallowPlayer);
            }

            // String re-mappings
            outstream.BeginArray("StringMappings", StringMappings.Count);
            foreach (var pair in StringMappings)
            {
                outstream.BeginObject();
                outstream.WriteStringProperty("Key", pair.Key);
                outstream.WriteStringProperty("Rule", pair.Value.Rule);
                outstream.WriteStringProperty("NewValue", pair.Value.NewValue);
                outstream.EndObject();
            }

            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Core stats
            ObjectName = instream.ReadStringProperty("ObjectName");
            ObjectAlias = instream.ReadStringProperty("ObjectAlias");
            Strength = instream.ReadInt32Property("Strength");
            Agility = instream.ReadInt32Property("Agility");
            Body = instream.ReadInt32Property("Body");
            Wits = instream.ReadInt32Property("Wits");
            Flags = instream.ReadInt32Property("Flags");
            Level = instream.ReadInt32Property("Level");
            Size = instream.ReadEnumProperty<ESize>("Size");
            Gender = instream.ReadEnumProperty<EGender>("Gender");

            // Equipment links
            if (version >= 11)
            {
                Equipment[0] = instream.ReadGuidProperty("Equipment1");
                Equipment[1] = instream.ReadGuidProperty("Equipment2");
                Equipment[2] = instream.ReadGuidProperty("Equipment3");
                Equipment[3] = instream.ReadGuidProperty("Equipment4");
            }

            // Vore stats
            PredatorEnabled = instream.ReadBooleanProperty("IsPredator");
            if (PredatorEnabled && version <= 10)
            {
                // Version 10
                PredatorDigests = instream.ReadBooleanProperty("DigestsPrey");
            }
            else if (PredatorEnabled)
            {
                // Version 11
                PredatorDigests = instream.ReadBooleanProperty("PredatorDigests");
                PredatorDisposal = instream.ReadBooleanProperty("PredatorDisposal");
                PredatorAlwaysSwallowPlayer = instream.ReadBooleanProperty("PredatorAlwaysSwallowPlayer");
            }

            // String re-mappings
            for (int count = instream.BeginArray("StringMappings"); count > 0; count--)
            {
                instream.BeginObject();
                {
                    string key = instream.ReadStringProperty("Key");
                    string rule = instream.ReadStringProperty("Rule");
                    string value = instream.ReadStringProperty("NewValue");
                    StringMappings.Add(key, new StringMapping
                    {
                        Rule = rule,
                        NewValue = value
                    });
                }
                instream.EndObject();
            }
            instream.EndArray();
        }

    }

}
