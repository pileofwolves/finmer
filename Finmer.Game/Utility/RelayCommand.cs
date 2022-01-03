/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Finmer.Utility
{

    /// <summary>
    /// Represents an <seealso cref="ICommand" /> that invokes a delegate upon execution.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RelayCommand : ICommand
    {

        private readonly Predicate<object> m_CanExecute;
        private readonly Action<object> m_Execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The delegate to invoke upon command execution.</param>
        /// <param name="canExecute">The predicate to use to check whether the command is enabled.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_CanExecute = canExecute;
        }

        /// <summary>
        /// The can-execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Returns a value indicating whether the <see cref="RelayCommand" /> can be executed.
        /// </summary>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute(parameter);
        }

        /// <summary>
        /// Executes the command and invokes the bound delegate.
        /// </summary>
        public void Execute(object parameter)
        {
            m_Execute(parameter);
        }

    }

}
