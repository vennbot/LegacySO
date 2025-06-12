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
using Microsoft.Xna.Framework;

namespace FSO.Common.Rendering.Framework.Camera
{
    public interface ICamera
    {
        Matrix View { get; }
        Matrix Projection { get; }

        Vector3 Position { get; set; }
        Vector3 Target { get; set; }
        Vector3 Up { get; set; }
        Vector3 Translation { get; set; }

        Vector2 ProjectionOrigin { get; set; }
        float NearPlane { get; set; }
        float FarPlane { get; set; }
        float Zoom { get; set; }
        float AspectRatioMultiplier { get; set; }

        void ProjectionDirty();

    }
}
