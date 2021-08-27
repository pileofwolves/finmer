/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    internal sealed class CompassGridViewModel : BaseProp
    {

        private RelayCommand m_DirCommand;

        public ICommand DirectionalLinkCommand => m_DirCommand ??
            (m_DirCommand = new RelayCommand(DirectionalLinkExecute, DirectionalLinkCheck));

        private void DirectionalLinkExecute(object args)
        {
            // Design mode check
            if (Application.Current.MainWindow == null)
                return;

            var dir = (CompassDirection)Enum.Parse(typeof(CompassDirection), (string)args);
            GameUI.Instance.PerformDirectionalLink(dir);
        }

        private bool DirectionalLinkCheck(object args)
        {
            // Design mode check
            if (Application.Current.MainWindow == null)
                return false;

            var dir = (CompassDirection)Enum.Parse(typeof(CompassDirection), (string)args);
            return GameUI.Instance.DirectionalLinks.ContainsKey(dir);
        }

    }

}
