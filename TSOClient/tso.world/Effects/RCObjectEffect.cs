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
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.LotView.Effects
{
    public class RCObjectEffect : LightMappedEffect
    {
        protected override Type TechniqueType
        {
            get { return typeof(RCObjectTechniques); }
        }

        private EffectParameter pWorld;
        private EffectParameter pViewProjection;

        private EffectParameter pObjectID;
        private EffectParameter pUVScale;
        private EffectParameter pAmbientLight;
        private EffectParameter pSideMask;

        private EffectParameter pMeshTex;
        private EffectParameter pAnisoTex;
        private EffectParameter pMaskTex;

        public Matrix World
        {
            set
            {
                pWorld.SetValue(value);
            }
        }
        public Matrix ViewProjection
        {
            set
            {
                pViewProjection.SetValue(value);
            }
        }

        public float ObjectID
        {
            set
            {
                pObjectID.SetValue(value);
            }
        }
        public Vector2 UVScale
        {
            set
            {
                pUVScale.SetValue(value);
            }
        }
        public Vector4 AmbientLight
        {
            set
            {
                pAmbientLight.SetValue(value);
            }
        }
        public float SideMask
        {
            set
            {
                pSideMask.SetValue(value);
            }
        }

        public Texture2D MeshTex
        {
            set
            {
                pMeshTex.SetValue(value);
            }
        }
        public Texture2D AnisoTex
        {
            set
            {
                if (pAnisoTex == null) pMeshTex.SetValue(value);
                else pAnisoTex.SetValue(value);
            }
        }
        public Texture2D MaskTex
        {
            set
            {
                pMaskTex.SetValue(value);
            }
        }

        public RCObjectEffect(Effect cloneSource) : base(cloneSource)
        {
        }

        public RCObjectEffect(GraphicsDevice graphicsDevice, byte[] effectCode) : base(graphicsDevice, effectCode)
        {
        }

        public RCObjectEffect(GraphicsDevice graphicsDevice, byte[] effectCode, int index, int count) : base(graphicsDevice, effectCode, index, count)
        {
        }

        protected override void PrepareParams()
        {
            base.PrepareParams();
            pWorld = Parameters["World"];
            pViewProjection = Parameters["ViewProjection"];

            pObjectID = Parameters["ObjectID"];
            pUVScale = Parameters["UVScale"];
            pAmbientLight = Parameters["AmbientLight"];
            pSideMask = Parameters["SideMask"];

            pMeshTex = Parameters["MeshTex"];
            pAnisoTex = Parameters["AnisoTex"];
            pMaskTex = Parameters["MaskTex"];
        }

        public void SetTechnique(RCObjectTechniques technique)
        {
            SetTechnique((int)technique);
        }
    }

    public enum RCObjectTechniques
    {
        Draw = 0,
        DepthClear,
        DisabledDraw,
        WallDraw,
        WallLMap,
        LMapDraw,
    }
}
