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

        private static readonly Image CoinImage = new Image
        {
            Margin = new Thickness(6, 3, 0, 0),
            Stretch = Stretch.None,
            MinWidth = 16,
            MinHeight = 16,
            SnapsToDevicePixels = true,
            UseLayoutRounding = true,

            Source =
                new BitmapImage(new Uri("pack://application:,,,/Finmer;component/Resources/UI/Money.png", UriKind.Absolute))
                {
                    CacheOption = BitmapCacheOption.OnLoad
                }
        };

        public ItemTooltipView()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(CoinImage, BitmapScalingMode.NearestNeighbor);
            CoinImage.Source.Freeze();
        }

        private void UserControl_Loaded(object sender, EventArgs e)
        {
            var item = (Item)DataContext;
            InlineCollection inl = ItemInfoLabel.Inlines;
            inl.Clear();

            // disconnect coin icon
            var coin_parent = CoinImage.Parent as Canvas;
            coin_parent?.Children.Remove(CoinImage);

            // handle empty slots
            var parent = (UIElement)Parent;
            parent.Visibility = Visibility.Visible;
            if (item == null)
            {
                parent.Visibility = Visibility.Hidden;
                return;
            }

            // item name
            AssetItem asset = item.Asset;
            inl.Add(new Bold(new Run(asset.ObjectName)));
            inl.Add(new LineBreak());

            if (asset.IsQuestItem)
            {
                inl.Add("Quest Item");
                inl.Add(new LineBreak());
            }

            // specialized stats
            switch (asset.ItemType)
            {
                case AssetItem.EItemType.Weapon:
                    inl.Add("Weapon");
                    inl.Add(new LineBreak());
                    break;

                case AssetItem.EItemType.Armor:
                    inl.Add("Armor");
                    inl.Add(new LineBreak());
                    break;

                case AssetItem.EItemType.Generic:
                    // if generic already has quest tag, don't write similar text lines
                    if (!asset.IsQuestItem)
                    {
                        inl.Add("Item");
                        inl.Add(new LineBreak());
                    }

                    break;

                case AssetItem.EItemType.Usable:
                    inl.Add(new LineBreak());

                    string usable_text = !String.IsNullOrWhiteSpace(asset.UseDescription)
                        ? "Usable: " + asset.UseDescription
                        : "Usable";
                    inl.Add(new Run(usable_text) { Foreground = new SolidColorBrush(Theme.LogColorPositive) });
                    inl.Add(new LineBreak());

                    break;
            }

            // general stats
            inl.Add(new LineBreak());

            if (asset.PurchaseValue > 0)
            {
                var coin_canvas = new Canvas();
                coin_canvas.Children.Add(CoinImage);
                coin_canvas.Width = 25;
                coin_canvas.Height = 16;
                inl.Add("Value:");
                inl.Add(coin_canvas);
                inl.Add(asset.PurchaseValue.ToString());
                inl.Add(new LineBreak());
            }

            // write flavor text at the end, if any
            if (!String.IsNullOrEmpty(asset.FlavorText))
            {
                inl.Add(new LineBreak());
                inl.Add(new LineBreak());
                inl.Add(new Italic(new Run(asset.FlavorText))
                {
                    Foreground = new SolidColorBrush(Theme.LogColorLightGray)
                });
            }
        }

    }

}
