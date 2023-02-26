/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Assets;
using Finmer.Core.Buffs;
using Finmer.Gameplay;
using Finmer.Utility;
using System;
using System.ComponentModel;
using System.Text;
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
            // This function is called from the XAML designer; ignore the callback in that case
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

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

            // Describe all effect groups provided by this item
            foreach (var effect in asset.EquipEffects)
                DescribeEquipmentEffectGroup(parts, effect);
        }

        private static void DescribeEquipmentEffectGroup(InlineCollection parts, EquipEffectGroup group)
        {
            // Spacing with previous group
            parts.Add(new LineBreak());
            parts.Add(new LineBreak());

            // Style of the trigger event
            StringBuilder header_text = new StringBuilder(256);
            switch (group.ProcStyle)
            {
                case EquipEffectGroup.EProcStyle.Always:                header_text.Append("While equipped"); break;
                case EquipEffectGroup.EProcStyle.RoundStart:            header_text.Append("When a new round starts"); break;
                case EquipEffectGroup.EProcStyle.TurnStart:             header_text.Append("When your turn starts"); break;
                case EquipEffectGroup.EProcStyle.WielderAttackHit:      header_text.Append("When hitting an opponent"); break;
                case EquipEffectGroup.EProcStyle.WielderAttackMiss:     header_text.Append("When missing an opponent"); break;
                case EquipEffectGroup.EProcStyle.WielderGrappled:       header_text.Append("When grappling"); break;
                case EquipEffectGroup.EProcStyle.WielderSwallowed:      header_text.Append("When swallowed"); break;
                case EquipEffectGroup.EProcStyle.WielderSwallowsPrey:   header_text.Append("When prey is swallowed"); break;
                case EquipEffectGroup.EProcStyle.EnemyAttackHit:        header_text.Append("When hit by opponent"); break;
                case EquipEffectGroup.EProcStyle.EnemyAttackMiss:       header_text.Append("When opponent misses"); break;
            }

            // 'Always-on' effects don't need further context; other styles have trigger conditions
            if (group.ProcStyle != EquipEffectGroup.EProcStyle.Always)
            {
                header_text.Append(",");

                // If the effect has a randomized chance to trigger, display that number
                if (group.ProcChance < 1.0f)
                    header_text.AppendFormat(" {0:F0}% chance to", group.ProcChance * 100.0f);

                // Show to which target the effects will apply
                header_text.AppendLine();
                header_text.Append("apply to ");
                switch (group.ProcTarget)
                {
                    case EquipEffectGroup.EProcTarget.Self:             header_text.Append("self"); break;
                    case EquipEffectGroup.EProcTarget.Opponent:         header_text.Append("opponent"); break;
                    case EquipEffectGroup.EProcTarget.AllAllies:        header_text.Append("all allies"); break;
                    case EquipEffectGroup.EProcTarget.AllOpponents:     header_text.Append("all opponents"); break;
                }

                // Effect duration
                header_text.AppendFormat(" for {0} round{1}", group.Duration, group.Duration == 1 ? String.Empty : "s");
            }

            // Draw the generated header text on-screen
            header_text.Append(":");
            parts.Add(header_text.ToString());

            // Next, describe each of the buffs actually contained in this effect group
            foreach (var buff in group.Buffs)
                DescribeEquipmentBuff(parts, buff);
        }

        private static void DescribeEquipmentBuff(InlineCollection parts, Buff buff)
        {
            // Spacing with the previous item
            parts.Add(new LineBreak());

            // Describe the individual buff
            switch (buff)
            {
                case BuffAttackDice effect_attack:
                    DescribeEquipmentBuffDiceTrack(parts, effect_attack, SimpleDiceTrack.EDiceStyle.Attack, "Attack Dice");
                    break;

                case BuffDefenseDice effect_defense:
                    DescribeEquipmentBuffDiceTrack(parts, effect_defense, SimpleDiceTrack.EDiceStyle.Defense, "Defense Dice");
                    break;

                case BuffGrappleDice effect_grapple:
                    DescribeEquipmentBuffDiceTrack(parts, effect_grapple, SimpleDiceTrack.EDiceStyle.D6, "Grapple Dice");
                    break;

                case BuffSwallowDice effect_swallow:
                    DescribeEquipmentBuffDiceTrack(parts, effect_swallow, SimpleDiceTrack.EDiceStyle.D6, "Swallow Dice");
                    break;

                case BuffStruggleDice effect_struggle:
                    DescribeEquipmentBuffDiceTrack(parts, effect_struggle, SimpleDiceTrack.EDiceStyle.D6, "Struggle Dice");
                    break;

                case BuffHealth effect_health:
                    // Show the added/removed health points
                    var info = new TextBlock
                    {
                        Text = $"{effect_health.Delta:+##,###;-##,###;0} Max Health",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    parts.Add(CreateBuffContainer(buff, $"{effect_health.Delta:+#;-#;0} Health", info));
                    break;

                case BuffCustomTooltipText effect_text:
                    parts.Add(CreateBuffContainer(buff, effect_text.TooltipText, null));
                    break;
            }
        }

        private static void DescribeEquipmentBuffDiceTrack(InlineCollection parts, SingleDeltaBuff buff, SimpleDiceTrack.EDiceStyle style, string label)
        {
            // Show a dice track indicating the added/removed dice
            var track = new SimpleDiceTrack
            {
                DiceCount = buff.Delta,
                DiceStyle = style
            };
            parts.Add(CreateBuffContainer(buff, $"{buff.Delta:+#;-#;0} {label}", track));
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
