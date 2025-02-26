/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core
{

    /// <summary>
    /// Describes a dependency on another asset package.
    /// </summary>
    public struct FurballDependency : IEquatable<FurballDependency>
    {

        /// <summary>
        /// The GUID of the dependency package.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The package file name under which the GUID was last seen. May not be accurate; this is primarily used for UI display.
        /// </summary>
        public string FileNameHint { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FurballDependency other && Equals(other);
        }

        public bool Equals(FurballDependency other)
        {
            return ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator==(FurballDependency left, FurballDependency right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(FurballDependency left, FurballDependency right)
        {
            return !left.Equals(right);
        }

    }

}
