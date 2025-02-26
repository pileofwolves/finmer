/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    public partial class BandedListView : UserControl
    {

        private static readonly Brush k_RowBackgroundBrush1 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        private static readonly Brush k_RowBackgroundBrush2 = new SolidBrush(Color.FromArgb(255, 232, 239, 244));
        private static readonly Brush k_RowHoverBrush2 = new SolidBrush(Color.FromArgb(255, 215, 222, 229));
        private static readonly Brush k_RowFocusBrush2 = new SolidBrush(Color.FromArgb(255, 5, 124, 245));

        /// <summary>
        /// .
        /// </summary>
        public int SelectedIndex
        {
            get => listView.SelectedIndices.Count == 1 ? listView.SelectedIndices[0] : -1;
            set
            {
                listView.SelectedIndices.Clear();

                if (value != -1)
                    listView.Items[value].Selected = true;
            }
        }

        /// <summary>
        /// .
        /// </summary>
        public ListViewItem SelectedItem => listView.SelectedItems.Count == 1 ? listView.SelectedItems[0] : null;

        /// <summary>
        /// Gets the collection of list items.
        /// </summary>
        public ListView.ListViewItemCollection Items => listView.Items;

        /// <summary>
        /// .
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// .
        /// </summary>
        public event EventHandler ItemDoubleClick;

        public BandedListView()
        {
            InitializeComponent();
        }

        public static Color ConvertColor(ScriptNode.EColor color)
        {
            switch (color)
            {
                case ScriptNode.EColor.System:              return Color.FromArgb(255, 128, 128, 128);
                case ScriptNode.EColor.Code:                return Color.FromArgb(255, 64, 64, 64);
                case ScriptNode.EColor.Comment:             return Color.ForestGreen;
                case ScriptNode.EColor.FlowControl:         return Color.FromArgb(255, 40, 67, 204);
                case ScriptNode.EColor.Message:             return Color.FromArgb(255, 109, 14, 155);
                case ScriptNode.EColor.SceneControl:        return Color.FromArgb(255, 67, 145, 255);
                case ScriptNode.EColor.SaveData:            return Color.FromArgb(255, 5, 129, 131);
                case ScriptNode.EColor.Player:              return Color.FromArgb(255, 144, 3, 11);
                case ScriptNode.EColor.Journal:             return Color.FromArgb(255, 165, 139, 57);
                case ScriptNode.EColor.Time:                return Color.FromArgb(255, 40, 114, 60);
                case ScriptNode.EColor.Variable:            return Color.FromArgb(255, 198, 23, 38);
                case ScriptNode.EColor.Sleep:               return Color.FromArgb(255, 230, 24, 81);
                case ScriptNode.EColor.Combat:              return Color.FromArgb(255, 166, 19, 220);
                default:                                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Pick a brush for the background
            Brush back_brush;
            if (e.Item.Selected)
                back_brush = k_RowFocusBrush2;
            else if (e.Item.Tag != null && listView.RectangleToScreen(e.Bounds).Contains(Cursor.Position))
                back_brush = k_RowHoverBrush2;
            else if (e.ItemIndex % 2 == 1)
                back_brush = k_RowBackgroundBrush1; // Alternating colors for regular rows
            else
                back_brush = k_RowBackgroundBrush2;

            // Draw the background
            e.Graphics.FillRectangle(back_brush, e.Bounds);

            // Offset text slightly to the left, for aesthetics
            var text_offset = e.Item.IndentCount * 28 + 2;
            var text_bounds = e.Bounds;
            text_bounds.X += text_offset;
            text_bounds.Width -= text_offset;

            // Make selected text a different color, for readability against the dark background
            var text_color = e.Item.Selected ? Color.White : e.Item.ForeColor;

            // Draw text
            TextRenderer.DrawText(e.Graphics, e.Item.Text, e.Item.Font, text_bounds, text_color, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            columnHeader.Width = listView.ClientSize.Width;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Forward event
            SelectedIndexChanged?.Invoke(sender, e);
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            // Forward event to host
            ItemDoubleClick?.Invoke(sender, e);
        }

    }

}
