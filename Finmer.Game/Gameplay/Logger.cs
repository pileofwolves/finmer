/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Finmer.Core;
using Finmer.Models;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Provides an interface for writing to an output log file.
    /// </summary>
    internal static class Logger
    {

        private static readonly DirectoryInfo s_BaseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

        /// <summary>
        /// Verifies if the calling thread has permission to write a file to the application directory.
        /// </summary>
        public static bool HasWritePermission()
        {
            var permission_set = new PermissionSet(PermissionState.Unrestricted);
            var write_permission = new FileIOPermission(FileIOPermissionAccess.Write, s_BaseDir.FullName);
            permission_set.AddPermission(write_permission);

            return permission_set.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
        }

        /// <summary>
        /// Writes a text file to the application directory describing an <see cref="Exception" />.
        /// </summary>
        /// <param name="ex">The <see cref="Exception" /> to describe.</param>
        public static void WriteExceptionReport(Exception ex)
        {
            // If we can't write the file at all, 
            if (!HasWritePermission())
                return;

            // Generate a base file name for the dump
            string base_name = Path.Combine(s_BaseDir.FullName, "crash" + DateTime.Now.ToFileTime());

            // First of all, try writing a minidump to disk, since that contains the most useful info
            try
            {
                using (var minidump = new FileStream(base_name + ".dmp", FileMode.Create))
                {
                    Minidump.Write(minidump.SafeFileHandle, Minidump.EMinidumpOptions.Default, Minidump.EExceptionInfo.Present);
                }
            }
            catch (Exception)
            {
                // Ignore
            }

            // Write a text crash log that includes a stacktrace and some game state
            try
            {
                using (var crash_report = new StreamWriter(base_name + ".log"))
                {
                    crash_report.WriteLine("===================================");
                    crash_report.WriteLine("EXCEPTION REPORT");
                    crash_report.WriteLine();

                    WriteBasicInfo(ex, crash_report);
                    crash_report.WriteLine();

                    WriteExceptionInfo(ex, crash_report);
                    crash_report.WriteLine();

                    WriteModuleInfo(crash_report);
                    crash_report.WriteLine();

                    WriteSaveDataInfo(crash_report);
                    crash_report.WriteLine();

                    WriteUserConfigInfo(crash_report);
                    crash_report.WriteLine();

                    crash_report.WriteLine("END OF FILE");
                    crash_report.WriteLine("===================================");
                }
            }
            catch (Exception)
            {
                // Ignore
            }
        }

        private static void WriteUserConfigInfo(StreamWriter crashlog)
        {
            crashlog.WriteLine("Active userconfig:");

            // Serialize the user config as a byte stream
            PropertyBag conf = UserConfig.Flush();
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                {
                    conf.Serialize(writer);
                }

                crashlog.WriteLine(Convert.ToBase64String(ms.ToArray()));
            }
        }

        private static void WriteExceptionInfo(Exception ex, StreamWriter crashlog)
        {
            crashlog.WriteLine(ex);
        }

        private static void WriteBasicInfo(Exception ex, StreamWriter crashlog)
        {
            crashlog.WriteLine($"Game Version: {CompileConstants.k_VersionString}");
            crashlog.WriteLine($"Timestamp: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            crashlog.WriteLine($"OS Version: {Environment.OSVersion} (64-bit OS: {Environment.Is64BitOperatingSystem} / 64-bit process: {Environment.Is64BitProcess})");
            crashlog.WriteLine($"CLR Version: {Environment.Version}");
            crashlog.WriteLine($"Memory (working set): {Environment.WorkingSet}");
            crashlog.WriteLine($"Memory (page size): {Environment.SystemPageSize}");
            crashlog.WriteLine($"Target Site: {ex.TargetSite}");
        }

        private static void WriteModuleInfo(StreamWriter crashlog)
        {
            crashlog.WriteLine("Loaded modules:");
            GameController.LoadedModules.ForEach(furball => { crashlog.WriteLine($"- '{furball.Title}' by {furball.Author} [{furball.ID}]"); });
        }

        private static void WriteSaveDataInfo(StreamWriter crashlog)
        {
            crashlog.WriteLine("Loaded player:");

            Player player = GameController.Session?.Player;
            if (player != null)
            {
                // Serialize the current player as a byte stream
                PropertyBag save_data = player.SaveState();
                using (var ms = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                    {
                        save_data.Serialize(writer);
                    }

                    crashlog.WriteLine(Convert.ToBase64String(ms.ToArray()));
                }
            }
            else
            {
                // Otherwise, we have no save data loaded
                crashlog.WriteLine("[unloaded]");
            }
        }

    }

}
