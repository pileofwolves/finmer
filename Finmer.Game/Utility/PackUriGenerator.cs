/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Utility
{

    /// <summary>
    /// Provides utilities for building URIs referencing embedded resources or content.
    /// </summary>
    public static class PackUriGenerator
    {

        private const string k_PathDelimiter = "/";
        private const string k_PackAuthority = "application:,,,";
        private const string k_GameResourcesBase = "Finmer;component/Resources";

        private static readonly string k_PackBase = "pack" + Uri.SchemeDelimiter + k_PackAuthority + k_PathDelimiter;

        /// <summary>
        /// Returns a Uri suitable for addressing the specified game resource file.
        /// </summary>
        /// <param name="relative">Path and file name of the resource, relative to the Resources folder.</param>
        public static Uri GetGameResource(string relative)
        {
            // If the relative path does not start with a slash, add it now
            if (!relative.StartsWith(k_PathDelimiter, StringComparison.InvariantCultureIgnoreCase))
                relative = k_PathDelimiter + relative;

            // Build the absolute path
            string path = k_PackBase + k_GameResourcesBase + relative;
            return new Uri(path, UriKind.Absolute);
        }

    }

}
