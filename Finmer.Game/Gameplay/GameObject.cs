/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        private string m_Alias;
        private string m_Name;
        private EGender m_Gender;

        protected GameObject(ScriptContext context, PropertyBag template) : base(context, template)
        {
            m_Name = template.GetString(SaveData.k_Object_Name);
            m_Alias = template.GetString(SaveData.k_Object_Alias);

            // Alias defaults to name if unspecified
            if (String.IsNullOrWhiteSpace(Alias))
                m_Alias = m_Name;

            Gender = (EGender)template.GetInt(SaveData.k_Object_Gender, (int)EGender.Ungendered);
        }

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
        public string PronounObjective { get; protected set; }

        [TextProperty(@"subject")]
        public string PronounSubjective { get; protected set; }

        [TextProperty(@"possessive")]
        public string PronounPossessive { get; protected set; }

        [TextProperty(@"object3p")]
        public string PronounObjective3P { get; protected set; }

        [TextProperty(@"subject3p")]
        public string PronounSubjective3P { get; protected set; }

        [TextProperty(@"possessive3p")]
        public string PronounPossessive3P { get; protected set; }

        [TextProperty(@"aliaspossessive")]
        public string AliasPossessive { get ; protected set; }

        [TextProperty(@"namepossessive")]
        public string NamePossessive { get ; protected set; }

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

        public override PropertyBag SerializeProperties()
        {
            var props = base.SerializeProperties();
            props.SetString(SaveData.k_Object_Name, Name);
            props.SetString(SaveData.k_Object_Alias, Alias);
            props.SetInt(SaveData.k_Object_Gender, (int)Gender);

            return props;
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
