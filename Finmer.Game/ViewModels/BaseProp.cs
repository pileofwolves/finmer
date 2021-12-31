/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Base class for view models.
    /// </summary>
    public abstract class BaseProp : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Make a copy of the invocation list, to avoid race conditions
            var handler = PropertyChanged;
            if (handler != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                foreach (Delegate subscriber in handler.GetInvocationList())
                {
                    // If the target requires synchronization, use an indirect invocation to cross thread boundaries. Otherwise, invoke directly.
                    if (subscriber.Target is DispatcherObject dispatch && !dispatch.CheckAccess())
                        dispatch.Dispatcher.BeginInvoke(subscriber, this, args);
                    else
                        subscriber.DynamicInvoke(this, args);
                }
            }
        }

    }

}
