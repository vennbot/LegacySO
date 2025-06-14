
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
using FSO.Common.Rendering.Framework.Camera;
using Microsoft.Xna.Framework;
using System;

namespace FSO.LotView.Utils.Camera
{
    public class DummyCamera : ICamera
    {
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public Vector3 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Up { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Translation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 ProjectionOrigin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float NearPlane { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float FarPlane { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Zoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AspectRatioMultiplier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ProjectionDirty()
        {
        }
    }
}
