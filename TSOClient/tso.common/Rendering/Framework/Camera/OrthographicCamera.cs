
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Common.Rendering.Framework.Camera
{
    /// <summary>
    /// Orthographic camera for the game. Used for rendering lots.
    /// </summary>
    public class OrthographicCamera : BasicCamera
    {
        public OrthographicCamera(GraphicsDevice device, Vector3 Position, Vector3 Target, Vector3 Up) 
            : base(device, Position, Target, Up)
        {
        }

        protected override void CalculateProjection()
        {
            var device = m_Device;
            var aspect = device.Viewport.AspectRatio * AspectRatioMultiplier;

            var ratioX = m_ProjectionOrigin.X / device.Viewport.Width;
            var ratioY = m_ProjectionOrigin.Y / device.Viewport.Height;

            var projectionX = 0.0f - (1.0f * ratioX);
            var projectionY = (1.0f * ratioY);

            m_Projection = Matrix.CreateOrthographicOffCenter(
                projectionX, projectionX + 1.0f,
                ((projectionY - 1.0f) / aspect), (projectionY) / aspect,
                NearPlane, FarPlane
            );

            var zoom = 1 / m_Zoom;
            m_Projection = m_Projection * Matrix.CreateScale(zoom);
        }
    }
}
