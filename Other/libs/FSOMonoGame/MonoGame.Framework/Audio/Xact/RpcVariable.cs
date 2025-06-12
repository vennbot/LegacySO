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

namespace Microsoft.Xna.Framework.Audio
{
    struct RpcVariable 
    {
        public string Name;
        public float Value;
        public byte Flags;
        public float InitValue;
        public float MaxValue;
        public float MinValue;

        public bool IsPublic
        {
            get { return (Flags & 0x1) != 0; }
        }

        public bool IsReadOnly
        {
            get { return (Flags & 0x2) != 0; }
        }

        public bool IsGlobal
        {
            get { return (Flags & 0x4) == 0; }
        }

        public bool IsReserved
        {
            get { return (Flags & 0x8) != 0; }
        }

        public void SetValue(float value)
        {
            if (value < MinValue)
                Value = MinValue;
            else if (value > MaxValue)
                Value = MaxValue;
            else
                Value = value;
        }
    }
}
