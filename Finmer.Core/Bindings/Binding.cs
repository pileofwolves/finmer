/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Text;

namespace Finmer.Core.Bindings
{

    /// <summary>
    /// </summary>
    public abstract class Binding<T>
    {

        /// <summary>
        /// Retrieves the backing value of this binding.
        /// </summary>
        public abstract T Get();

        /// <summary>
        /// Updates the backing value of this binding. May not be supported for all types of bindings.
        /// </summary>
        /// <param name="value">The new value to write.</param>
        /// <exception cref="NotSupportedException">
        /// Exception thrown if the underlying binding type does not support direct
        /// writing.
        /// </exception>
        public virtual void Set(T value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Implicitly casts this binding to the backing type with the backing value, as if Get had been called.
        /// </summary>
        public static implicit operator T(Binding<T> binding)
        {
            return binding.Get();
        }

        protected abstract EBindingType Type();
        protected abstract void OnSerialize(BinaryWriter writer);
        protected abstract void OnDeserialize(BinaryReader reader);

        private void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)Type());
            OnSerialize(writer);
        }

        public byte[] ToBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms, Encoding.UTF8))
                {
                    Serialize(writer);
                    return ms.ToArray();
                }
            }
        }

        public static Binding<T> FromBytes(byte[] serialized)
        {
            return null;
        }

    }

}
