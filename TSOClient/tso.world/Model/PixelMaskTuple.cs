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
using Microsoft.Xna.Framework.Graphics;

namespace FSO.LotView.Model
{
    struct PixelMaskTuple
    {
        public Texture2D Pixel;
        public Texture2D Mask;

        public PixelMaskTuple(Texture2D px, Texture2D mask)
        {
            Pixel = px;
            Mask = mask;
        }

        public override int GetHashCode()
        {
            return (Pixel?.GetHashCode()??0) ^ (Mask?.GetHashCode()??0);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PixelMaskTuple)obj;
            return (Pixel == other.Pixel && Mask == other.Mask);
        }
    }
}
