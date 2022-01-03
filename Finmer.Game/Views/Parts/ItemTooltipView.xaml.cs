/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Assets;
using Finmer.Core.Buffs;
using Finmer.Gameplay;
using Finmer.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private void ItemTooltipView_OnContextChanged(object sender, DependencyPropertyChangedEventArgs e) 
        {
            if (DataContext != null)
                CreateTooltipContent(((Item)DataContext).Asset, ItemInfoLabel.Inlines);
        }

        private static void CreateTooltipContent(AssetItem asset, InlineCollection parts)
        {
            //Clear out the cached tooltip content
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
                    DescribeEquipableItem(asset, parts);
                    break;

                case AssetItem.EItemType.Generic:
                    DescribeGenericItem(asset, parts);
                    break;

                case AssetItem.EItemType.Usable:
                    DescribeUsableItem(asset, parts);
                    break;
            }

            // Monetary value
            if (asset.PurchaseValue > 0)
            {
                parts.Add(new LineBreak());
                parts.Add(new LineBreak());
                parts.Add("Value:");
                parts.Add(CreateCoinImage());
                parts.Add($"{asset.PurchaseValue:##,###}");
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

        private static void DescribeUsableItem(AssetItem asset, InlineCollection parts)
        {
            parts.Add("Usable");

            // If a Use Description was specified in the editor, include it here
            if (!String.IsNullOrWhiteSpace(asset.UseDescription))
            {
                // Add the text as a standalone paragraph
                parts.Add(new LineBreak());
                parts.Add(new LineBreak());
                parts.Add(new Run("When used: " + asset.UseDescription) { Foreground = new SolidColorBrush(Theme.LogColorPositive) });
            }
        }

        private static void DescribeGenericItem(AssetItem asset, InlineCollection parts)
        {
            // Write a tag indicating this is a generic item (but avoid writing it again if it's also a Quest Item)
            if (!asset.IsQuestItem)
                parts.Add("Item");
        }

        private static void DescribeEquipableItem(AssetItem asset, InlineCollection parts)
        {
            // Write the name of the required equip slot
            parts.Add(asset.EquipSlot.ToString());

            // Show equipment effects
            if (asset.EquipEffects.Count != 0)
            {
                // Header text
                parts.Add(new LineBreak());
                parts.Add(new LineBreak());
                parts.Add("When equipped:");

                // Describe all buffs provided by this item
                foreach (var effect in asset.EquipEffects)
                {
                    parts.Add(new LineBreak());
                    DescribeEquipmentBuff(parts, effect);
                }
            }
        }

        private static void DescribeEquipmentBuff(InlineCollection parts, Buff effect)
        {
            switch (effect)
            {
                case BuffAttackDice effect_attack:
                    // Show a dice track indicating the added/removed dice
                    var track_attack = new SimpleDiceTrack();
                    track_attack.DiceCount = effect_attack.Delta;
                    track_attack.DiceStyle = SimpleDiceTrack.EDiceStyle.Attack;
                    parts.Add(CreateBuffContainer(effect, $"{effect_attack.Delta:+#;-#;0} Attack Dice", track_attack));
                    break;

                case BuffDefenseDice effect_defense:
                    // Show a dice track indicating the added/removed dice
                    var track_defense = new SimpleDiceTrack();
                    track_defense.DiceCount = effect_defense.Delta;
                    track_defense.DiceStyle = SimpleDiceTrack.EDiceStyle.Defense;
                    parts.Add(CreateBuffContainer(effect, $"{effect_defense.Delta:+#;-#;0} Defense Dice", track_defense));
                    break;

                case BuffHealth effect_health:
                    // Show the added/removed health points
                    var info = new TextBlock
                    {
                        Text = $"{effect_health.Delta:+##,###;-##,###;0} Max Health",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    parts.Add(CreateBuffContainer(effect, $"{effect_health.Delta:+#;-#;0} Health", info));
                    break;
            }
        }

        private static UIElement CreateBuffContainer(Buff buff, string description, UIElement child)
        {
            var container = new ItemTooltipBuffRow
            {
                RowImpact = buff.GetImpact(),
                RowLabel = description,
                RowWidget = child
            };

            return container;
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
