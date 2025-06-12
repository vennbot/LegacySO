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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
    public struct HalfVector2 : IPackedVector<uint>, IPackedVector, IEquatable<HalfVector2>
    {
        private uint packedValue;
        public HalfVector2(float x, float y)
        {
            this.packedValue = PackHelper(x, y);
        }

        public HalfVector2(Vector2 vector)
        {
            this.packedValue = PackHelper(vector.X, vector.Y);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = PackHelper(vector.X, vector.Y);
        }

        private static uint PackHelper(float vectorX, float vectorY)
        {
            uint num2 = HalfTypeHelper.Convert(vectorX);
            uint num = (uint)(HalfTypeHelper.Convert(vectorY) << 0x10);
            return (num2 | num);
        }

        public Vector2 ToVector2()
        {
            Vector2 vector;
            vector.X = HalfTypeHelper.Convert((ushort)this.packedValue);
            vector.Y = HalfTypeHelper.Convert((ushort)(this.packedValue >> 0x10));
            return vector;
        }

        Vector4 IPackedVector.ToVector4()
        {
            Vector2 vector = this.ToVector2();
            return new Vector4(vector.X, vector.Y, 0f, 1f);
        }

        [CLSCompliant(false)]
        public uint PackedValue
        {
            get
            {
                return this.packedValue;
            }
            set
            {
                this.packedValue = value;
            }
        }
        public override string ToString()
        {
            return this.ToVector2().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((obj is HalfVector2) && this.Equals((HalfVector2)obj));
        }

        public bool Equals(HalfVector2 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        public static bool operator ==(HalfVector2 a, HalfVector2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HalfVector2 a, HalfVector2 b)
        {
            return !a.Equals(b);
        }
    }
}
