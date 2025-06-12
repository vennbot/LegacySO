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
    struct RpcCurve
    {
        public uint FileOffset;
        public int Variable;
        public bool IsGlobal;
        public RpcParameter Parameter;
        public RpcPoint[] Points;

        public float Evaluate(float position)
        {
            // TODO: We need to implement the different RpcPointTypes.

            var first = Points[0];
            if (position <= first.Position)
                return first.Value;

            var second = Points[Points.Length - 1];
            if (position >= second.Position)
                return second.Value;

            for (var i = 1; i < Points.Length; ++i)
            {
                second = Points[i];
                if (second.Position >= position)
                    break;

                first = second;
            }

            switch (first.Type)
            {
                default:
                case RpcPointType.Linear:
                {
                    var t = (position - first.Position) / (second.Position - first.Position);
                    return first.Value + ((second.Value - first.Value) * t);
                }
            }
        }
    }
}
