/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;
using Finmer.Gameplay.Scripting;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an object that has a presence in the game world.
    /// </summary>
    public abstract class GameObject : ScriptableObject
    {

        private string m_Alias = String.Empty;
        private string m_Name = String.Empty;
        private EGender m_Gender;

        protected GameObject(ScriptContext context) : base(context) {}

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public EGender Gender
        {
            get => m_Gender;
            set
            {
                m_Gender = value;
                ReloadPronouns();
                OnPropertyChanged();
            }
        }

        [TextProperty(@"object")]
        public string PronounObjective { get; protected set; } = String.Empty;

        [TextProperty(@"subject")]
        public string PronounSubjective { get; protected set; } = String.Empty;

        [TextProperty(@"possessive")]
        public string PronounPossessive { get; protected set; } = String.Empty;

        [TextProperty(@"object3p")]
        public string PronounObjective3P { get; protected set; } = String.Empty;

        [TextProperty(@"subject3p")]
        public string PronounSubjective3P { get; protected set; } = String.Empty;

        [TextProperty(@"possessive3p")]
        public string PronounPossessive3P { get; protected set; } = String.Empty;

        [TextProperty(@"aliaspossessive")]
        public string AliasPossessive { get ; protected set; } = String.Empty;

        [TextProperty(@"namepossessive")]
        public string NamePossessive { get ; protected set; } = String.Empty;

        /// <summary>
        /// The UI-friendly name of this object.
        /// </summary>
        [TextProperty(@"name")]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
                ReloadPronouns();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// An alternate UI-friendly name for this object, usually used for text generation.
        /// </summary>
        [TextProperty(@"alias")]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public string Alias
        {
            get => m_Alias;
            set
            {
                // Avoid recursion
                if (m_Alias.Equals(value))
                    return;

                m_Alias = value;
                ReloadPronouns();
                OnPropertyChanged();
            }
        }

        public override PropertyBag SaveState()
        {
            var output = base.SaveState();

            output.SetString(SaveData.k_Object_Name, Name);
            output.SetString(SaveData.k_Object_Alias, Alias);
            output.SetInt(SaveData.k_Object_Gender, (int)Gender);

            return output;
        }

        public override void LoadState(PropertyBag input)
        {
            base.LoadState(input);

            m_Name = input.GetString(SaveData.k_Object_Name);
            m_Alias = input.GetString(SaveData.k_Object_Alias);

            // Alias defaults to name if unspecified
            if (String.IsNullOrWhiteSpace(Alias))
                m_Alias = m_Name;

            Gender = (EGender)input.GetInt(SaveData.k_Object_Gender, (int)EGender.Ungendered);
        }

        protected virtual void ReloadPronouns()
        {
            PronounObjective3P = GenderUtil.GetObjectivePronoun(m_Gender);
            PronounSubjective3P = GenderUtil.GetSubjectivePronoun(m_Gender);
            PronounPossessive3P = GenderUtil.GetPossessivePronoun(m_Gender);

            // Most characters use third-person pronouns
            PronounObjective = PronounObjective3P;
            PronounSubjective = PronounSubjective3P;
            PronounPossessive = PronounPossessive3P;
            AliasPossessive = Alias + (Alias.EndsWith("s") ? "'" : "'s");
            NamePossessive = Name + (Name.EndsWith("s") ? "'" : "'s");
        }

    }

}
