/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using JetBrains.Annotations;

namespace Finmer.Utility
{

    /// <summary>
    /// A collection of extension methods.
    /// </summary>
    internal static class Extensions
    {

        /// <summary>
        /// Iterates over a collection and invokes the specified delegate on each item.
        /// </summary>
        /// <param name="source">The collection to iterate through.</param>
        /// <param name="fn">The function to invoke for each item.</param>
        public static void ForEach<TResult>([InstantHandle] this IEnumerable<TResult> source, [InstantHandle] Action<TResult> fn)
        {
            TResult[] copy = source.ToArray(); // copy protects against enumerable modification errors
            foreach (TResult item in copy) fn(item);
        }

        /// <summary>
        /// Gets the first child of the specified type attached to a parent, doing a depth-first search.
        /// </summary>
        /// <typeparam name="T">The type of tree object to find.</typeparam>
        /// <param name="parent">The parent in which to start looking.</param>
        public static T GetVisualChild<T>(this DependencyObject parent) where T : Visual
        {
            T child = default;

            int num_visuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < num_visuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);

                if (child != null) break;
            }

            return child;
        }

    }

}
