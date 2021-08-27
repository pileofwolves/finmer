/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.CompilerServices;
using Finmer.Core;
using Finmer.Gameplay.Scripting;
using Finmer.ViewModels;

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
            m_Name = template.GetString("name");
            m_Alias = template.GetString("alias");

            // Alias defaults to name if unspecified
            if (String.IsNullOrWhiteSpace(Alias))
                m_Alias = m_Name;

            Gender = (EGender)Enum.Parse(typeof(EGender), template.GetString("gender", "Neuter"));
        }

        /// <summary>
        /// Gets or sets the view model instance associated with this game object.
        /// </summary>
        private WeakReference<GameObjectViewModel> ViewModel { get; set; }

        /// <summary>
        /// Returns either a cached viewmodel that wraps this model, or a newly created one. This function must be called on the UI thread.
        /// </summary>
        public GameObjectViewModel GetOrCreateViewModel()
        {
            // Try to find a cached viewmodel to return
            if (ViewModel == null || !ViewModel.TryGetTarget(out GameObjectViewModel output))
            {
                // If it's unavailable, create and cache a new viewmodel
                output = CreateViewModel();
                ViewModel = new WeakReference<GameObjectViewModel>(output);
            }

            return output;
        }

        /// <summary>
        /// Notifies the cached viewmodel, if any, that a property on the (view)model has changed values.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Try to get the viewmodel associated with this object
            if (ViewModel == null || !ViewModel.TryGetTarget(out GameObjectViewModel output))
                return;

            // Relay the change notification to the viewmodel, which will schedule it for the UI thread
            output.RelayPropertyChanged(propertyName);
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

        [TextProperty("object")]
        public string PronounObjective { get; protected set; }

        [TextProperty("subject")]
        public string PronounSubjective { get; protected set; }

        [TextProperty("possessive")]
        public string PronounPossessive { get; protected set; }

        [TextProperty("object3p")]
        public string PronounObjective3P { get; protected set; }

        [TextProperty("subject3p")]
        public string PronounSubjective3P { get; protected set; }

        [TextProperty("possessive3p")]
        public string PronounPossessive3P { get; protected set; }

        [TextProperty("aliaspossessive")]
        public string AliasPossessive { get ; protected set; }

        [TextProperty("namepossessive")]
        public string NamePossessive { get ; protected set; }

        /// <summary>
        /// The UI-friendly name of this object.
        /// </summary>
        [TextProperty("name")]
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
        [TextProperty("alias")]
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
            props.SetString("name", Name);
            props.SetString("alias", Alias);
            props.SetString("gender", Gender.ToString());

            return props;
        }

        protected abstract GameObjectViewModel CreateViewModel();

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
