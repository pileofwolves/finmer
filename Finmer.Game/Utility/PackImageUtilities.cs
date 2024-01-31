/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Media.Imaging;

namespace Finmer.Utility
{

    /// <summary>
    /// Provides utilities for loading images from embedded resources.
    /// </summary>
    public static class PackImageUtilities
    {

        /// <summary>
        /// Creates and returns a bitmap from the specified embedded resource URI.
        /// </summary>
        public static BitmapImage GetImage(Uri uri)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.UriSource = uri;
            source.EndInit();
            source.Freeze();
            return source;
        }

        /// <summary>
        /// Creates and returns a bitmap from the embedded resource identified using a relative resource path.
        /// </summary>
        public static BitmapImage GetImage(string relative)
        {
            return GetImage(PackUriGenerator.GetGameResource(relative));
        }

    }

}
