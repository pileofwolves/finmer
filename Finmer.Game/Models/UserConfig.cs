/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Text;
using Finmer.Core;

namespace Finmer.Models
{

    /// <summary>
    /// Manages persistent user-specific information.
    /// </summary>
    public static class UserConfig
    {

        /// <summary>
        /// Describes user preference for animation displays.
        /// </summary>
        public enum EAnimationLevel
        {
            Full,
            Quick,
            Disabled
        }

        public const float k_Zoom_Min = 1.0f;
        public const float k_Zoom_Max = 1.5f;

        private const uint k_ConfigMagic = 0xF1CF0001;
        private const string k_ConfigFileName = "Settings.sav";

        /// <summary>
        /// Indicates whether word breaking is enabled in the game log.
        /// </summary>
        public static bool Hyphenation { get; set; } = true;

        /// <summary>
        /// Indicates whether explicit disposal content is enabled.
        /// </summary>
        public static bool PreferScat { get; set; } = true;

        /// <summary>
        /// Indicates whether PreySense is enabled prior to vore scenes
        /// </summary>
        public static bool PreySense { get; set; } = false;

        /// <summary>
        /// Indicates whether this is the application's first time starting up.
        /// </summary>
        public static bool FirstStart { get; set; } = true;

        /// <summary>
        /// The relative UI zoom multiplier.
        /// </summary>
        public static float Zoom { get; set; } = 1.0f;

        /// <summary>
        /// The cached initial save data to be restored when opening the character creator.
        /// </summary>
        public static PropertyBag NewGamePreset { get; set; }

        /// <summary>
        /// The desired combat animation speed.
        /// </summary>
        public static EAnimationLevel CombatAnimation { get; set; } = EAnimationLevel.Full;

        /// <summary>
        /// Load user settings from disk, overwriting any cached settings.
        /// </summary>
        public static void Reload()
        {
            try
            {
                // Skip disk access if the config file doesn't exist at all
                if (!File.Exists(k_ConfigFileName))
                    return;

                using (var fs = new FileStream(k_ConfigFileName, FileMode.Open))
                {
                    using (var instream = new BinaryReader(fs, Encoding.UTF8, true))
                    {
                        // Verify magic number
                        uint conf_version = instream.ReadUInt32();
                        if (conf_version != k_ConfigMagic)
                            return;

                        // Deserialize the file
                        PropertyBag props = PropertyBag.FromStream(instream);

                        // Extract properties we're interested in
                        Hyphenation = props.GetBool(@"hyph");
                        PreferScat = props.GetBool(@"scat");
                        PreySense = props.GetBool(@"preysense");
                        FirstStart = props.GetBool(@"firststart");
                        Zoom = Math.Min(Math.Max(props.GetFloat(@"zoom"), k_Zoom_Min), k_Zoom_Max);
                        NewGamePreset = props.GetNestedPropertyBag(@"last_character");
                        CombatAnimation = (EAnimationLevel)Math.Min(Math.Max(props.GetInt(@"combatanim"), (int)EAnimationLevel.Full), (int)EAnimationLevel.Disabled);
                    }
                }
            }
            catch (IOException)
            {
                // Ignore exception
            }
        }

        /// <summary>
        /// Save the current user settings to disk.
        /// </summary>
        public static void Save()
        {
            try
            {
                using (var fs = new FileStream(k_ConfigFileName, FileMode.Create))
                {
                    using (var outstream = new BinaryWriter(fs, Encoding.UTF8, true))
                    {
                        // File version number
                        outstream.Write(k_ConfigMagic);

                        // Property bag underneath
                        PropertyBag props = Flush();
                        props.Serialize(outstream);
                    }
                }
            }
            catch (IOException)
            {
                // Ignore exception
            }
        }

        /// <summary>
        /// Generate a PropertyBag that contains a snapshot of the user settings.
        /// </summary>
        public static PropertyBag Flush()
        {
            var props = new PropertyBag();
            props.SetString(@"game_version", CompileConstants.k_VersionString);
            props.SetBool(@"hyph", Hyphenation);
            props.SetBool(@"scat", PreferScat);
            props.SetBool(@"preysense", PreySense);
            props.SetBool(@"firststart", FirstStart);
            props.SetFloat(@"zoom", Zoom);
            props.SetNestedPropertyBag(@"last_character", NewGamePreset);
            props.SetInt(@"combatanim", (int)CombatAnimation);
            return props;
        }

    }

}
