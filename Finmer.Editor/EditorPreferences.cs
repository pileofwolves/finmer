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

namespace Finmer.Editor
{

    /// <summary>
    /// Manages persistent user-specific information.
    /// </summary>
    public static class EditorPreferences
    {

        private const uint k_ConfigMagic = 0xF1CF0001;
        private const string k_ConfigFileName = "Editor.sav";

        /// <summary>
        /// Path to the executable to launch when starting the game.
        /// </summary>
        public static string ExecutablePath { get; set; } = "Finmer.exe";

        /// <summary>
        /// Working directory to apply to the launched game executable, or an empty string if unset.
        /// </summary>
        public static string ExecutableWorkingDirectory { get; set; } = String.Empty;

        /// <summary>
        /// Reloads the user preferences from disk.
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
                        ExecutablePath = props.GetString("exe_path");
                        ExecutableWorkingDirectory = props.GetString("exe_workdir");
                    }
                }
            }
            catch (IOException)
            {
                // Ignore exception
            }
        }

        /// <summary>
        /// Saves the current user preferences to disk.
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
        /// Serializes the state of the user preferences to a PropertyBag.
        /// </summary>
        private static PropertyBag Flush()
        {
            var props = new PropertyBag();
            props.SetString("exe_path", ExecutablePath);
            props.SetString("exe_workdir", ExecutableWorkingDirectory);
            return props;
        }

    }

}
