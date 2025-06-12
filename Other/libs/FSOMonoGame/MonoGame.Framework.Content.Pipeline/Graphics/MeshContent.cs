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

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides properties and methods that define various aspects of a mesh.
    /// </summary>
    public class MeshContent : NodeContent
    {
        GeometryContentCollection geometry;
        PositionCollection positions;

        /// <summary>
        /// Gets the list of geometry batches for the mesh.
        /// </summary>
        public GeometryContentCollection Geometry
        {
            get
            {
                return geometry;
            }
        }

        /// <summary>
        /// Gets the list of vertex position values.
        /// </summary>
        public PositionCollection Positions
        {
            get
            {
                return positions;
            }
        }

        /// <summary>
        /// Initializes a new instance of MeshContent.
        /// </summary>
        public MeshContent()
        {
            geometry = new GeometryContentCollection(this);
            positions = new PositionCollection();
        }

        /// <summary>
        /// Applies a transform directly to position and normal channels. Node transforms are unaffected.
        /// </summary>
        internal void TransformContents(ref Matrix xform)
        {
            // Transform positions
            for (int i = 0; i < positions.Count; i++)
                positions[i] = Vector3.Transform(positions[i], xform);

            // Transform all vectors too:
            // Normals are "tangent covectors", which need to be transformed using the
            // transpose of the inverse matrix!
            Matrix inverseTranspose = Matrix.Transpose(Matrix.Invert(xform));
            foreach (var geom in geometry)
            {
                foreach (var channel in geom.Vertices.Channels)
                {
                    var vector3Channel = channel as VertexChannel<Vector3>;
                    if (vector3Channel == null)
                        continue;

                    if (channel.Name.StartsWith("Normal") ||
                        channel.Name.StartsWith("Binormal") ||
                        channel.Name.StartsWith("Tangent"))
                    {
                        for (int i = 0; i < vector3Channel.Count; i++)
                        {
                            Vector3 normal = vector3Channel[i];
                            Vector3.TransformNormal(ref normal, ref inverseTranspose, out normal);
                            Vector3.Normalize(ref normal, out normal);
                            vector3Channel[i] = normal;
                        }
                    }
                }
            }

            // Swap winding order when faces are mirrored.
            if (MeshHelper.IsLeftHanded(ref xform))
                MeshHelper.SwapWindingOrder(this);
        }
    }
}
