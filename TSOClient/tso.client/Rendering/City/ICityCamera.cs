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
using FSO.Client.Controllers;
using FSO.Common.Rendering.Framework.Camera;
using FSO.Common.Rendering.Framework.Model;
using FSO.LotView;
using Microsoft.Xna.Framework;

namespace FSO.Client.Rendering.City
{
    public interface ICityCamera : ICamera
    {
        TerrainZoomMode Zoomed { get; set; }
        float LotZoomProgress { get; set; }
        float ZoomProgress { get; set; }
        float LotSquish { get; }
        float FogMultiplier { get; }
        float DepthBiasScale { get; }
        float FarUIFade { get; }
        CityCameraCenter CenterCam { get; set; }
        bool HideUI { get; }

        void Update(UpdateState state, Terrain city);
        void MouseEvent(FSO.Common.Rendering.Framework.IO.UIMouseEventType type, UpdateState state);
        float GetIsoScale();
        Vector2 CalculateR();
        Vector2 CalculateRShadow();
        void InheritPosition(Terrain parent, World lotWorld, CoreGameScreenController controller, bool instant);

        void CenterCamera(CityCameraCenter center);
        void ClearCenter();
    }

    public class CityCameraCenter
    {
        public Vector2 Center;
        public float YAngle;
        public float Dist;

        public float RotAngle;
        public int ID;
    }
}
