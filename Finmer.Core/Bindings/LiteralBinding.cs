/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;

namespace Finmer.Core.Bindings
{

    /// <inheritdoc />
    /// <summary>
    /// Represents a <seealso cref="T:Finmer.Core.Bindings.Binding`1" /> with a single direct backing value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LiteralBinding<T> : Binding<T>
    {

        private T m_Value;

        /// <summary>
        /// Creates a new <see cref="LiteralBinding{T}" /> with the value initialized to the type default.
        /// </summary>
        public LiteralBinding()
        {
            m_Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="LiteralBinding{T}" /> with an explicit default value.
        /// </summary>
        /// <param name="def">The value to initialize this binding to.</param>
        public LiteralBinding(T def)
        {
            m_Value = def;
        }

        /// <inheritdoc />
        public override T Get()
        {
            return m_Value;
        }

        /// <inheritdoc />
        public override void Set(T value)
        {
            m_Value = value;
        }

        protected override EBindingType Type()
        {
            return EBindingType.Invalid;
        }

        protected override void OnSerialize(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        protected override void OnDeserialize(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

    }

}
