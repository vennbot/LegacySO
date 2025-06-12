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
/*This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
If a copy of the MPL was not distributed with this file, You can obtain one at
http://mozilla.org/MPL/2.0/.

The Original Code is the TSO LoginServer.

The Initial Developer of the Original Code is
Propeng. All Rights Reserved.

Contributor(s): Mats 'Afr0' Vederhus
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Iffinator.Flash
{
    public struct PixelOffset
    {
        public int X, Y;

        public PixelOffset(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public struct WorldOffset
    {
        public float X, Y, Z;

        public WorldOffset(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class DrawGroupSprite
    {
        private ushort m_Type;
        private uint m_Flags;
        private PixelOffset m_SpriteOffset;
        private WorldOffset m_ObjectOffset;
        private Bitmap m_Bitmap;
        private SpriteFrame m_Sprite;

        public ushort Type { get { return m_Type; } }
        public uint Flags { get { return m_Flags; } }
        public PixelOffset SpriteOffset { get { return m_SpriteOffset; } }
        public WorldOffset ObjectOffset { get { return m_ObjectOffset; } }
        public Bitmap Bitmap { get { return m_Bitmap; } }
        public SpriteFrame Sprite { get { return m_Sprite; } }

        public DrawGroupSprite(ushort type, uint flags, PixelOffset spriteOffset, WorldOffset objectOffset, SpriteFrame frame)
        {
            m_Type = type;
            m_Flags = flags;
            m_SpriteOffset = spriteOffset;
            m_ObjectOffset = objectOffset;
            m_Sprite = frame;

            m_Bitmap = (Bitmap)frame.BitmapData.BitMap.Clone();
            if ((m_Flags & 0x1) == 0x1)
            {
                m_Bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }
    }
}
