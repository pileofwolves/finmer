/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a temporary storage area and duplication tool for a Furball serializable asset.
    /// </summary>
    /// <typeparam name="TAsset">Underlying type of the copyable asset.</typeparam>
    internal class Clipboard<TAsset> where TAsset : class, IFurballSerializable
    {

        /// <summary>
        /// Occurs when the clipboard content has been modified.
        /// </summary>
        public event Action ContentChanged;

        private TAsset m_Buffer;

        /// <summary>
        /// Replace the current contents of the clipboard.
        /// </summary>
        public void Set(TAsset source)
        {
            m_Buffer = AssetSerializer.DuplicateAsset(source);
            ContentChanged?.Invoke();
        }

        /// <summary>
        /// Clear the current contents of the clipboard.
        /// </summary>
        public void Clear()
        {
            m_Buffer = default;
            ContentChanged?.Invoke();
        }

        /// <summary>
        /// Indicates whether the clipboard currently contains an object.
        /// </summary>
        public bool HasContent()
        {
            return m_Buffer != default;
        }

        /// <summary>
        /// Returns a reference to the clipboard contents.
        /// </summary>
        public TAsset PeekBuffer()
        {
            return m_Buffer;
        }

        /// <summary>
        /// Returns a deep copy of the clipboard contents.
        /// </summary>
        public TAsset CopyBuffer()
        {
            return m_Buffer != null ? AssetSerializer.DuplicateAsset(m_Buffer) : null;
        }

        /// <summary>
        /// Return the clipboard contents, and clear it.
        /// </summary>
        public TAsset ClaimBuffer()
        {
            TAsset temp = m_Buffer;
            m_Buffer = default;
            ContentChanged?.Invoke();

            return temp;
        }

    }

}
