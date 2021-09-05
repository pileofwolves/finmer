/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Finmer.Core.Assets;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemTooltipView.xaml
    /// </summary>
    public partial class ItemTooltipView
    {

        public ItemTooltipView()
        {
            InitializeComponent();
        }

        private void Tooltip_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Regenerate the tooltip
            if (DataContext != null)
                CreateTooltipContent(((Item)DataContext).Asset, ItemInfoLabel.Inlines);
        }

        private static void CreateTooltipContent(AssetItem asset, InlineCollection parts)
        {
            // Ensure we're not duplicating the tooltip contents
            parts.Clear();

            // Display the item name
            parts.Add(new Bold(new Run(asset.ObjectName)));
            parts.Add(new LineBreak());

            // Quest item marker
            if (asset.IsQuestItem)
            {
                parts.Add("Quest Item");
                parts.Add(new LineBreak());
            }

            // Type-specific stats
            switch (asset.ItemType)
            {
                case AssetItem.EItemType.Equipable:
                    parts.Add(asset.EquipSlot.ToString());
                    parts.Add(new LineBreak());
                    break;

                case AssetItem.EItemType.Generic:
                    // Write a tag indicating this is a generic item (but avoid writing it again if it's also a Quest Item)
                    if (!asset.IsQuestItem)
                    {
                        parts.Add("Item");
                        parts.Add(new LineBreak());
                    }

                    break;

                case AssetItem.EItemType.Usable:
                    // If a Use Description was specified in the editor, include it here
                    string usable_text = !String.IsNullOrWhiteSpace(asset.UseDescription)
                        ? "Usable: " + asset.UseDescription
                        : "Usable";

                    // Add the text as a standalone paragraph
                    parts.Add(new LineBreak());
                    parts.Add(new Run(usable_text) { Foreground = new SolidColorBrush(Theme.LogColorPositive) });
                    parts.Add(new LineBreak());

                    break;
            }

            // Monetary value
            if (asset.PurchaseValue > 0)
            {
                parts.Add(new LineBreak());
                parts.Add("Value:");
                parts.Add(CreateCoinImage());
                parts.Add($"{asset.PurchaseValue:##,###}");
                parts.Add(new LineBreak());
            }

            // Flavor text (if specified in editor)
            if (!String.IsNullOrEmpty(asset.FlavorText))
            {
                parts.Add(new LineBreak());
                parts.Add(new LineBreak());
                parts.Add(new Italic(new Run(asset.FlavorText))
                {
                    Foreground = new SolidColorBrush(Theme.LogColorLightGray)
                });
            }
        }

        private static UIElement CreateCoinImage()
        {
            // Generate a new icon object
            var output = new Image
            {
                Margin = new Thickness(6, 3, 0, 0),
                Stretch = Stretch.None,
                MinWidth = 16,
                MinHeight = 16,
                SnapsToDevicePixels = true,
                UseLayoutRounding = true,

                Source = new BitmapImage(PackUriGenerator.GetGameResource("UI/Money.png"))
                {
                    CacheOption = BitmapCacheOption.OnLoad
                }
            };

            // Avoid blurry scaling
            RenderOptions.SetBitmapScalingMode(output, BitmapScalingMode.NearestNeighbor);

            // Avoid unnecessary image copies between CPU and GPU
            output.Source.Freeze();

            // Wrap the image in a grid so it can have a relative position
            var container = new Canvas
            {
                Width = 25,
                Height = 16
            };
            container.Children.Add(output);

            return container;
        }

    }

}
