
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
using FSO.LotView.Effects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FSO.LotView.Utils
{
    /// <summary>
    /// A group of sprites grouped by texture. In SoftwareDepth mode, each buffer has more than one of these. Otherwise it just tends to be one.
    /// </summary>
    public class _2DDrawBuffer : IDisposable
    {
        public List<_2DDrawGroup> Groups = new List<_2DDrawGroup>();

        public void Dispose()
        {
            foreach (var group in Groups)
            {
                group.Dispose();
            }
        }
    }

    public class _2DDrawGroup : IDisposable
    {
        public int Primitives;
        public VertexBuffer VertBuf;
        public IndexBuffer IndexBuf;
        public short[] Indices;
        public _2DSpriteVertex[] Vertices;

        public Texture2D Pixel;
        public Texture2D Depth;
        public Texture2D Mask;
        public WorldBatchTechniques Technique;
        public bool Floors;

        public void Dispose()
        {
            if (VertBuf == null) return;
            VertBuf.Dispose();
            IndexBuf.Dispose();
        }
    }
}
