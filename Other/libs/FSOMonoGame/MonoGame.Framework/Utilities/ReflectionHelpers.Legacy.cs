// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Utilities
{
    internal static partial class ReflectionHelpers
    {
        /// <summary>
        /// Generics handler for Marshal.SizeOf
        /// </summary>
        internal static class SizeOf<T>
        {
            static int _sizeOf;

            static SizeOf()
            {
                var type = typeof(T);
                _sizeOf = Marshal.SizeOf(type);
            }

            static public int Get()
            {
                return _sizeOf;
            }
        }

        /// <summary>
        /// Fallback handler for Marshal.SizeOf(type)
        /// </summary>
        internal static int ManagedSizeOf(Type type)
        {
            return Marshal.SizeOf(type);
        }
    }
}
