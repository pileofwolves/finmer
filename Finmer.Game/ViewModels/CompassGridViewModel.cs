/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the compass button widget.
    /// </summary>
    public sealed class CompassGridViewModel : BaseProp
    {

        /// <summary>
        /// Command for activating a directional link.
        /// </summary>
        public ICommand DirectionalLinkCommand => m_CommandLink ??
            (m_CommandLink = new RelayCommand(DirectionalLinkExecute, DirectionalLinkCheck));

        private ICommand m_CommandLink;

        private void DirectionalLinkExecute(object args)
        {
            var session = GameController.Session;

            // Session may be null in the XAML designer
            if (session == null || args == null)
                return;

            var direction = (ECompassDirection)args;
            session.Compass.QueueExecuteLink(direction);
        }

        private bool DirectionalLinkCheck(object args)
        {
            var session = GameController.Session;

            // Session may be null in the XAML designer
            if (session == null || args == null)
                return false;

            var direction = (ECompassDirection)args;
            return session.Compass.HasLink(direction);
        }

    }

}
