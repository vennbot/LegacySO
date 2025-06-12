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
    public struct HalfSingle : IPackedVector<UInt16>, IEquatable<HalfSingle>, IPackedVector
    {
        UInt16 packedValue;

        public HalfSingle(float single)
        {
            packedValue = HalfTypeHelper.Convert(single);
        }

        [CLSCompliant(false)]
        public ushort PackedValue
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

        public float ToSingle()
        {
            return HalfTypeHelper.Convert(this.packedValue);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = HalfTypeHelper.Convert(vector.X);
        }

        Vector4 IPackedVector.ToVector4()
        {
            return new Vector4(this.ToSingle(), 0f, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (HalfSingle)obj;
            }

            return false;
        }

        public bool Equals(HalfSingle other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.ToSingle().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
