/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Finmer.Core;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Provides tools for transforming grammar tags into readable, context-aware text.
    /// </summary>
    /// <remarks>
    /// This enables authors to write a gender- and subject-neutral text template, and then substitute words into the final
    /// text on the fly depending on the input grammar context. For example, the same text template could be reused for
    /// different verb forms or pronoun sets.
    /// </remarks>
    internal static class TextParser
    {

        private static readonly Dictionary<string, string> s_Variables = new Dictionary<string, string>();
        private static readonly Dictionary<string, Context> s_Contexts = new Dictionary<string, Context>();

        /// <summary>
        /// Adds or replaces a substitutable variable with the specified value.
        /// </summary>
        /// <param name="key">The key of the value. This is what game text should reference in a grammar tag.</param>
        /// <param name="value">The text that the grammar tag will be replaced with.</param>
        public static void SetVariable(string key, string value)
        {
            // Add a prefix/postfix to the key so it looks like a grammar tag
            string grammar_tag = $"{{{{!{key}}}}}";

            // Register the tag
            s_Variables.Remove(grammar_tag);
            s_Variables.Add(grammar_tag, value);
        }

        /// <summary>
        /// Adds or replaces a context object with the specified key.
        /// </summary>
        /// <param name="key">The key templates can use to address this context.</param>
        /// <param name="subject">The context object to register.</param>
        /// <param name="persist">If true, the context will persist through ClearContext calls.</param>
        public static void SetContext(string key, GameObject subject, bool persist)
        {
            if (s_Contexts.ContainsKey(key))
                s_Contexts.Remove(key);

            s_Contexts.Add(key, new Context(subject, persist));
        }

        /// <summary>
        /// Removes all non-persistent grammar contexts.
        /// </summary>
        public static void ClearNonPersistentContexts()
        {
            s_Variables.Clear();
            s_Contexts
                .Where(context => !context.Value.IsPersistent)
                .ForEach(context => s_Contexts.Remove(context.Key));
        }

        /// <summary>
        /// Removes all grammar contexts.
        /// </summary>
        public static void ClearAllContexts()
        {
            s_Variables.Clear();
            s_Contexts.Clear();
        }

        /// <summary>
        /// Parses a text template and replaces all tags with configured substitutes and contexts.
        /// </summary>
        /// <param name="raw">The unmodified template whose tags to substitute.</param>
        public static string Parse(string raw)
        {
            // Insert newlines
            raw = raw.Replace("\\n", Environment.NewLine);

            // Apply variable substitutions
            s_Variables.ForEach(pair => raw = raw.Replace(pair.Key, pair.Value));

            // Handle grammar tags
            var index_end = 0;
            while (true)
            {
                // Search the text for tags enclosed by double curly braces
                int index_start = raw.IndexOf("{{", index_end, StringComparison.InvariantCulture);
                if (index_start == -1) break;
                index_end = raw.IndexOf("}}", index_start, StringComparison.InvariantCulture);
                if (index_end == -1) break;

                // Handle this grammar tag
                string command = raw.Substring(index_start + 2, index_end - index_start - 2);
                string replacement = ProcessGrammarTag(command);

                // Remove the raw grammar tag, and inject the generated replacement
                Debug.Assert(replacement != null);
                raw = raw.Remove(index_start, index_end - index_start + 2);
                raw = raw.Insert(index_start, replacement);
                index_end = index_start + replacement.Length;
            }

            return raw;
        }

        /// <summary>
        /// Adds a global replacement key/value pair for all future Parse calls.
        /// </summary>
        private static void SetSubstitute(string key, string value)
        {
            if (s_Variables.ContainsKey(key))
                s_Variables.Remove(key);

            s_Variables.Add(key, value);
        }

        private static string ProcessGrammarTag(string command)
        {
            // Randomizer command
            if (command.StartsWith("?"))
            {
                // Find randomizer substrings, separated with a pipe character
                string[] random_parts = command.Split(new [] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (random_parts.Length >= 2)
                {
                    // Get rid of the ? at the start
                    random_parts = random_parts.Skip(1).ToArray();

                    // Pick a random entry
                    return random_parts[CoreUtility.Rng.Next(random_parts.Length)];
                }

                return "{{invalid randomizer expression}}";
            }

            // This is a context-based command; find the context key and parameter
            int index_split = command.IndexOfAny(new[] { ' ', '.' });
            if (index_split == -1)
                index_split = command.Length; // Handled below

            // Find the context for this key
            string context_key = command.Substring(0, index_split);
            if (!s_Contexts.TryGetValue(context_key, out Context context))
                return $"{{{{undefined context '{context_key}'}}}}";

            // Short form: If no split is found, and only the context name is present, then assume the Alias property is desired.
            if (index_split == command.Length)
                return GetProperty(context, "alias");

            // Get the parameter after the dot/space
            string parameter = command.Substring(index_split + 1, command.Length - index_split - 1);
            if (String.IsNullOrWhiteSpace(parameter))
                return "{{invalid parameter}}";

            // Property access with a dot
            if (command[index_split] == '.')
                return GetProperty(context, parameter);

            // Otherwise (with a space), assume verb conjugation is desired
            return Conjugate(context.Subject.PronounSubjective, parameter);
        }

        private static string GetProperty(Context context, string key)
        {
            if (context.Properties.TryGetValue(key, out PropertyInfo property))
            {
                string value = (string)property.GetValue(context.Subject);
                return value;
            }

            return $"{{{{context '{context.Subject.Name}' has no property '{key}'}}}}";
        }

        private static string Conjugate(string article, string verb)
        {
            // used for shorthand, because that StringComparison enum every time is annoying
            bool Eq(string lhs, string rhs)
            {
                return lhs.Equals(rhs, StringComparison.InvariantCulture);
            }

            article = article.ToLowerInvariant();
            verb = verb.ToLowerInvariant();

            // in english rules for (s)he/it are the same, so this makes life easier
            if (Eq(article, "he") || Eq(article, "she"))
                article = "it";

            // irregular verbs
            if (Eq(verb, "be"))
            {
                if (Eq(article, "i")) return "am";
                if (Eq(article, "you") || Eq(article, "we") || Eq(article, "they")) return "are";
                return "is";
            }

            if (Eq(verb, "do")) return Eq(article, "it") ? "does" : "do";

            if (verb.EndsWith("ss", StringComparison.InvariantCulture)
                || verb.EndsWith("o", StringComparison.InvariantCulture)
                || verb.EndsWith("sh", StringComparison.InvariantCulture)
                || verb.EndsWith("ch", StringComparison.InvariantCulture)
                || verb.EndsWith("tch", StringComparison.InvariantCulture)
                || verb.EndsWith("x", StringComparison.InvariantCulture)
                || verb.EndsWith("zz", StringComparison.InvariantCulture))
                return Eq(article, "it") ? verb + "es" : verb;

            // try, cry, pry, shy
            if (verb.EndsWith("ry", StringComparison.InvariantCulture)
                || verb.EndsWith("hy", StringComparison.InvariantCulture)
            )
                return Eq(article, "it") ? verb.Substring(0, verb.Length - 1) + "ies" : verb;

            // default
            if (article.Equals("it", StringComparison.InvariantCulture))
                return verb + "s";
            return verb;
        }

        /// <summary>
        /// Represents a grammar context, which acts as subject for verb conjugation or pronoun selection.
        /// </summary>
        private struct Context
        {

            public Context(GameObject subject, bool persistent)
            {
                Subject = subject;
                IsPersistent = persistent;
                Properties = new Dictionary<string, PropertyInfo>();

                PopulateProperties();
            }

            /// <summary>
            /// The subject to use.
            /// </summary>
            public GameObject Subject { get;  }

            /// <summary>
            /// Whether this context should be kept when performing regular context cleanup between user turns.
            /// </summary>
            public bool IsPersistent { get;  }

            /// <summary>
            /// Hashmap of grammar properties supported by this context.
            /// </summary>
            public Dictionary<string, PropertyInfo> Properties { get; }

            /// <summary>
            /// Cache the collection of text properties for this subject.
            /// </summary>
            private void PopulateProperties()
            {
                var all_properties = Subject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in all_properties)
                { 
                    var attr = property.GetCustomAttribute<TextPropertyAttribute>();
                    if (attr == null)
                        continue;

                    Properties.Add(attr.Key, property);
                }
            }

        }

    }

}
