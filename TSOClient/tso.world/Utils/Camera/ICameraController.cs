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
using FSO.Common.Rendering.Framework.Camera;
using FSO.Common.Rendering.Framework.Model;
using Microsoft.Xna.Framework;

namespace FSO.LotView.Utils.Camera
{
    public interface ICameraController
    {
        ICamera BaseCamera { get; }
        bool UseZoomHold { get; }
        bool UseRotateHold { get; }
        WorldRotation CutRotation { get; }

        void ZoomPress(float intensity);

        void ZoomHold(float intensity);

        void RotatePress(float intensity);

        void RotateHold(float intensity);

        void Update(UpdateState state, World world);

        void PreDraw(World world);

        ICameraController BeforeActive(ICameraController previous, World world);

        void OnActive(ICameraController previous, World world);

        void InvalidateCamera(WorldState state);

        void SetDimensions(Vector2 dim);
    }
}
