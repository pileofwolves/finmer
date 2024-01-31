/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Finmer.Views.Base
{

    /// <summary>
    /// Specialization of Image that observes the IsEnabled property, and converts the image to grayscale if the control is disabled.
    /// </summary>
    public class GrayscaleToggleImage : Image
    {

        static GrayscaleToggleImage()
        {
            // Attach callback to the IsEnabled property change event
            IsEnabledProperty.OverrideMetadata(typeof(GrayscaleToggleImage),
                new FrameworkPropertyMetadata(true, OnEnabledChanged));
        }

        private static void OnEnabledChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            // Sanity check: the below code only works if the disabled transition happens first
            if (args.OldValue.Equals(args.NewValue))
                return;

            // Obtain the image instance. May be null in the XAML designer.
            var image = source as GrayscaleToggleImage;
            if (image == null)
                return;

            // Is the button being toggled on or off?
            if (Convert.ToBoolean(args.NewValue))
            {
                // Button is enabled; reset changes that were made earlier
                image.Source = ((FormatConvertedBitmap)image.Source).Source;
                image.OpacityMask = null;
            }
            else
            {
                // Generate a grayscale image for the input image source
                var bitmap_source = (BitmapSource)image.Source;
                var gray_bitmap = new FormatConvertedBitmap(bitmap_source, PixelFormats.Gray8, null, 0);
                var gray_alpha = new ImageBrush(bitmap_source);

                // Ensure the generated image objects are not unnecessarily refreshed in GPU memory
                gray_bitmap.Freeze();
                gray_alpha.Freeze();

                // Apply grayscale image, along with the original image as alpha mask (since Gray8 format discards alpha)
                image.Source = gray_bitmap;
                image.OpacityMask = gray_alpha;
            }
        }

    }

}
