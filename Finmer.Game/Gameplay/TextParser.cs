/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

#if DEBUG
    // Enable this define to dump TextParser state to the game log
    //#define DEBUG_TEXT_PARSER
#endif

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
            string grammar_tag = $"{{!{key}}}";

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
        /// Given an input string table key, returns a string table key modified for the input characters' string mappings.
        /// </summary>
        /// <param name="input">The string table key to transform.</param>
        /// <param name="instigator">The character who performed some action.</param>
        /// <param name="target">The character who is the target of the instigator's action. Optional, may be null.</param>
        public static string EvaluateStringMappings(string input, Character instigator, Character target = null)
        {
            // Find a matching rule in the instigator's string mappings
            var asset = instigator.Asset;
            if (asset != null)
            {
                // Accept the first matching mapping
                foreach (var mapping in asset.StringMappings)
                    if (EvaluateStringMappingRule(mapping, input, instigator, target))
                        return mapping.NewKey;
            }

            // Repeat for the string mappings of the target
            asset = target?.Asset;
            if (asset != null)
            {
                // Accept the first matching mapping
                foreach (var mapping in asset.StringMappings)
                    if (EvaluateStringMappingRule(mapping, input, instigator, target))
                        return mapping.NewKey;
            }

            // No matching rule was found; return input unchanged
            return input;
        }

        /// <summary>
        /// Evaluates whether an individual StringMapping matches the input parameters. Returns true on matches, false otherwise.
        /// </summary>
        private static bool EvaluateStringMappingRule(StringMapping mapping, string input, Character instigator, Character target)
        {
            // The mapped input key must match
            if (!mapping.Key.Equals(input, StringComparison.InvariantCultureIgnoreCase))
                return false;

            // Verify the actual rule
            switch (mapping.Rule)
            {
                case StringMapping.ERule.Always:
                    return true;
                case StringMapping.ERule.NpcToPlayer:
                    return target is Player;
                case StringMapping.ERule.NpcToNpc:
                    return !(instigator is Player || target is Player);
                case StringMapping.ERule.PlayerToNpc:
                    return instigator is Player;
                default:
                    throw new ArgumentException(nameof(mapping));
            }
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
                // Search the text for tags enclosed by curly braces
                int index_start = raw.IndexOf('{', index_end);
                if (index_start == -1)
                    break;
                index_end = raw.IndexOf('}', index_start);
                if (index_end == -1)
                    break;

                // Handle this grammar tag
                string command = raw.Substring(index_start + 1, index_end - index_start - 1);
                string replacement = ProcessGrammarTag(command);

                // Remove the raw grammar tag, and inject the generated replacement
                Debug.Assert(replacement != null);
                raw = raw.Remove(index_start, index_end - index_start + 1);
                raw = raw.Insert(index_start, replacement);
                index_end = index_start + replacement.Length;
            }

            return raw;
        }

        /// <summary>
        /// Process a single grammar tag, returning the string it should be replaced with.
        /// </summary>
        private static string ProcessGrammarTag(string command)
        {
            // Prefix modifier: caret means we should capitalize the first character
            bool cap_first = false;
            if (command.StartsWith("^", StringComparison.InvariantCulture))
            {
                cap_first = true;
                command = command.Substring(1);
            }

            // Obtain the replacement string
            string replacement = ProcessGrammarTagInternal(command);

            // Handle modifiers
            if (cap_first)
                replacement = replacement.CapFirst();

            return replacement;
        }

        /// <summary>
        /// Process a single grammar tag that has its prefix modifiers removed.
        /// </summary>
        private static string ProcessGrammarTagInternal(string command)
        {
            // Randomized expression
            if (command.StartsWith("?", StringComparison.InvariantCulture))
                return HandleRandomizedTag(command);

            // This is a context-based command; find the context key and parameter
            int index_split = command.IndexOfAny(new[] { ' ', '.' });
            if (index_split == -1)
                index_split = command.Length; // Handled below

            // Find the context for this key
            string context_key = command.Substring(0, index_split);
            if (!s_Contexts.TryGetValue(context_key, out Context context))
                return $"{{undefined context '{context_key}'}}";

            // Short form: If no split is found, and only the context name is present, then assume the Alias property is desired.
            if (index_split == command.Length)
                return HandlePropertyTag(context, command, "alias");

            // Get the parameter after the dot/space
            string parameter = command.Substring(index_split + 1, command.Length - index_split - 1);
            if (String.IsNullOrWhiteSpace(parameter))
                return "{invalid parameter}";

            // Property access with a dot
            if (command[index_split] == '.')
                return HandlePropertyTag(context, command, parameter);

            // Otherwise (with a space), assume verb conjugation is desired.
            return HandleVerbTag(context, command, parameter);
        }

        private static string HandleRandomizedTag(string command)
        {
            // Find candidate substrings, separated with a pipe character
            string[] random_parts = command.Split(new [] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (random_parts.Length >= 2)
            {
                // Get rid of the ? at the start
                random_parts = random_parts.Skip(1).ToArray();

                // Pick a random entry
                string selection = random_parts[CoreUtility.Rng.Next(random_parts.Length)];
                PrintDebugInfo(command, selection);
                return selection;
            }

            PrintDebugInfo(command, "[invalid]");
            return "{invalid randomized expression}";
        }

        private static string HandlePropertyTag(Context context, string command, string key)
        {
            if (context.Properties.TryGetValue(key, out PropertyInfo property))
            {
                string value = (string)property.GetValue(context.Subject);
                PrintDebugInfo(context, command, key, value);
                return value;
            }

            PrintDebugInfo(context, command, key, "[invalid]");
            return $"{{context '{context.Subject.Name}' has no property '{key}'}}";
        }

        private static string HandleVerbTag(Context context, string command, string verb)
        {
            // Use second-person perspective for the player or for gender-neutral contexts. Note that the latter results in incorrect
            // grammar in some cases (i.e. "{foo} {foo be} nice" could resolve to "Bob are nice"), but this is currently unavoidable.
            var is_second_person = context.Subject is Player || context.Subject.Gender == EGender.Neutral;
            var conjugated = Conjugate(is_second_person ? EPerspective.SecondPerson : EPerspective.ThirdPerson, verb);

            PrintDebugInfo(context, command, "to " + verb, conjugated);
            return conjugated;
        }

        private static string Conjugate(EPerspective perspective, string verb)
        {
            verb = verb.ToLowerInvariant();

            // In English grammar, 'you' and 'they' use the same conjugations
            bool use_plural_form = perspective == EPerspective.SecondPerson;

            // Irregular verbs: to be
            if (verb.Equals("be", StringComparison.InvariantCulture))
                return use_plural_form ? "are" : "is";

            // Irregular verbs: to do
            if (verb.Equals("do", StringComparison.InvariantCulture))
                return use_plural_form ? "do" : "does";

            // Irregular verbs: to have
            if (verb.Equals("have", StringComparison.InvariantCulture))
                return use_plural_form ? "have" : "has";

            // Verbs conjugated with an additional 'e'
            if (verb.EndsWith("ss", StringComparison.InvariantCulture) ||
                verb.EndsWith("o", StringComparison.InvariantCulture) ||
                verb.EndsWith("sh", StringComparison.InvariantCulture) ||
                verb.EndsWith("ch", StringComparison.InvariantCulture) ||
                verb.EndsWith("tch", StringComparison.InvariantCulture) ||
                verb.EndsWith("x", StringComparison.InvariantCulture) ||
                verb.EndsWith("zz", StringComparison.InvariantCulture))
                return use_plural_form ? verb : verb + "es";

            // Verbs ending with 'y'
            if (verb.EndsWith("ry", StringComparison.InvariantCulture) ||
                verb.EndsWith("ly", StringComparison.InvariantCulture) ||
                verb.EndsWith("hy", StringComparison.InvariantCulture))
                return use_plural_form ? verb : verb.Substring(0, verb.Length - 1) + "ies";

            // Default
            return use_plural_form ? verb : verb + "s";
        }

        [Conditional(@"DEBUG_TEXT_PARSER")]
        private static void PrintDebugInfo(Context context, string command, string detail, string replacement)
        {
            string trace = $"{{ {command} (= {context.Subject.Name}, {detail}) -> {replacement} }}";
            Models.GameUI.Instance.Log(trace, Theme.LogColorLightGray);
        }

        [Conditional(@"DEBUG_TEXT_PARSER")]
        private static void PrintDebugInfo(string command, string replacement)
        {
            string trace = $"{{ {command} -> {replacement} }}";
            Models.GameUI.Instance.Log(trace, Theme.LogColorLightGray);
        }

        /// <summary>
        /// Selects which perspective form to use when conjugating a verb.
        /// </summary>
        private enum EPerspective
        {
            SecondPerson,
            ThirdPerson
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
            public GameObject Subject { get; }

            /// <summary>
            /// Whether this context should be kept when performing regular context cleanup between user turns.
            /// </summary>
            public bool IsPersistent { get; }

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
