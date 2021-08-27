/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;
using Finmer.Gameplay;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Represents a viewmodel that wraps a GameObject.
    /// </summary>
    /// <remarks>
    /// This split - i.e. why GameObject itself cannot be a BaseProp - is to ensure a clean split between 'business logic' (gameplay code)
    /// and the UI layer. The viewmodel object is therefore a thin wrapper around the properties that UI is interested in.
    /// </remarks>
    public class GameObjectViewModel : BaseProp
    {

        public GameObject GameObjectModel { get; }

        protected GameObjectViewModel(GameObject owner)
        {
            GameObjectModel = owner;
        }

        /// <summary>
        /// Schedule a property-changed event notification for execution on the UI thread.
        /// </summary>
        /// <param name="propertyName">The name of the property on the viewmodel that was changed.</param>
        public void RelayPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public string Name => GameObjectModel.Name;

        public string Alias => GameObjectModel.Alias;

        public EGender Gender => GameObjectModel.Gender;

    }

}
