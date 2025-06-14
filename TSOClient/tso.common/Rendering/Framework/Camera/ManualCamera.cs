
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
namespace FSO.Common.Rendering.Framework.Camera
{
    public class ManualCamera : ICamera
    {
        #region ICamera Members

        public Microsoft.Xna.Framework.Matrix View { get; set; }

        public Microsoft.Xna.Framework.Matrix Projection { get; set; }

        public Microsoft.Xna.Framework.Vector3 Position { get; set; }

        public Microsoft.Xna.Framework.Vector3 Target { get; set; }

        public Microsoft.Xna.Framework.Vector3 Up { get; set; }

        public Microsoft.Xna.Framework.Vector3 Translation { get; set; }

        public Microsoft.Xna.Framework.Vector2 ProjectionOrigin { get; set; }

        public float NearPlane { get; set; }

        public float FarPlane { get; set; }

        public float Zoom { get; set; }

        public float AspectRatioMultiplier { get; set; }

        public void ProjectionDirty()
        {
        }

        #endregion
    }
}
