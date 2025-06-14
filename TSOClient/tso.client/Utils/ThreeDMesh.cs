
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
using System.Collections.Generic;

namespace FSO.Client.Utils
{
    public class ThreeDMesh<T>
    {
        private List<T> Vertexes = new List<T>();
        private List<int> Indexes = new List<int>();
        private int IndexOffset = 0;
        private int _PrimitiveCount = 0;

        public void AddQuad(T tl, T tr, T br, T bl)
        {
            Vertexes.Add(tl);
            Vertexes.Add(tr);
            Vertexes.Add(br);
            Vertexes.Add(bl);

            Indexes.Add(IndexOffset);
            Indexes.Add(IndexOffset + 1);
            Indexes.Add(IndexOffset + 2);
            Indexes.Add(IndexOffset + 2);
            Indexes.Add(IndexOffset + 3);
            Indexes.Add(IndexOffset);

            IndexOffset += 4;
            _PrimitiveCount += 2;
        }

        public T[] GetVertexes()
        {
            return Vertexes.ToArray();
        }

        public int[] GetIndexes()
        {
            return Indexes.ToArray();
        }

        public int PrimitiveCount
        {
            get
            {
                return _PrimitiveCount;
            }
        }
    }
}
