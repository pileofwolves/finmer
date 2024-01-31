/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Finmer.Editor
{

    /// <summary>
    /// A container for extension methods.
    /// </summary>
    internal static class Extensions
    {

        /// <summary>
        /// Iterates over a collection and invokes the specified delegate on each item.
        /// </summary>
        /// <param name="source">The collection to iterate through.</param>
        /// <param name="fn">The function to invoke for each item.</param>
        public static void ForEach<TResult>(this IEnumerable<TResult> source, Action<TResult> fn)
        {
            TResult[] copy = source.ToArray(); // copy protects against enumerable modification errors
            foreach (TResult item in copy) fn(item);
        }

        /// <summary>
        /// Recursively iterates over a collection that has a tree hierarchy.
        /// </summary>
        /// <param name="items">The root element of the tree to traverse.</param>
        /// <param name="childSelector">A predicate that returns the children of a tree element.</param>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> childSelector)
        {
            Stack<T> stack = new Stack<T>(items);
            while (stack.Any())
            {
                T next = stack.Pop();
                yield return next;

                foreach (T child in childSelector(next))
                    stack.Push(child);
            }
        }

        /// <summary>
        /// Count the number of words in the specified string.
        /// </summary>
        public static int CountWords(this string str)
        {
            // If there is no meaningful text in this string, it contains no words either
            if (String.IsNullOrWhiteSpace(str))
                return 0;

            // We just use the number of spaces and escaped newlines
            int spaces = str.Count(ch => ch == ' ');
            int newlines = 0, index = str.IndexOf("\\n", StringComparison.InvariantCultureIgnoreCase);
            while (index != -1)
            {
                newlines++;
                index = str.IndexOf("\\n", index + 2, StringComparison.InvariantCultureIgnoreCase);
            }

            // Add one for the first word (i.e. one space separates two words)
            return spaces + newlines + 1;
        }

    }

}
