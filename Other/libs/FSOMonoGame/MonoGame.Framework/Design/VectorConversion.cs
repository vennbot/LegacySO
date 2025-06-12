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
using System.ComponentModel;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Microsoft.Xna.Framework.Design
{
    internal static class VectorConversion
    {
        public static bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(float))
                return true;
            if (destinationType == typeof(Vector2))
                return true;
            if (destinationType == typeof(Vector3))
                return true;
            if (destinationType == typeof(Vector4))
                return true;
            if (destinationType.GetInterface("IPackedVector") != null)
                return true;

            return false;
        }

        public static object ConvertToFromVector4(ITypeDescriptorContext context, CultureInfo culture, Vector4 value, Type destinationType)
        {
            if (destinationType == typeof(float))
                return value.X;
            if (destinationType == typeof(Vector2))
                return new Vector2(value.X, value.Y);
            if (destinationType == typeof(Vector3))
                return new Vector3(value.X, value.Y, value.Z);
            if (destinationType == typeof(Vector4))
                return new Vector4(value.X, value.Y, value.Z, value.W);
            if (destinationType.GetInterface("IPackedVector") != null)
            {
                var packedVec = (IPackedVector)Activator.CreateInstance(destinationType);
                packedVec.PackFromVector4(value);
                return packedVec;
            }            

            return null;
        }         
    }
}
