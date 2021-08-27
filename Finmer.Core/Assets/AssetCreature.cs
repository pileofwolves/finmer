/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

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

        public int Strength { get; set; }

        public int Agility { get; set; }

        public int Body { get; set; }

        public int Wits { get; set; }

        public int Flags { get; set; }

        public int Level { get; set; } = 1;

        public ESize Size { get; set; } = ESize.Medium;

        public EGender Gender { get; set; }

        public Dictionary<string, StringMapping> StringMappings { get; } = new Dictionary<string, StringMapping>();

        public bool PredatorEnabled { get; set; }

        public bool PredatorDigests { get; set; }

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

            // Vore stats
            outstream.WriteBooleanProperty("IsPredator", PredatorEnabled);
            if (PredatorEnabled)
            {
                outstream.WriteBooleanProperty("DigestsPrey", PredatorDigests);
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

            // Vore stats
            PredatorEnabled = instream.ReadBooleanProperty("IsPredator");
            if (PredatorEnabled)
            {
                PredatorDigests = instream.ReadBooleanProperty("DigestsPrey");
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
