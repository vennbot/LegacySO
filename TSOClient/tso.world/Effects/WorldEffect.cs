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
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;

namespace FSO.LotView.Effects
{
    public class WorldEffect : Effect
    {
        protected virtual Type TechniqueType
        {
            get { return typeof(LightingEmptyTechniques); }
        }

        protected EffectTechnique[] IndexedTechniques;

        protected WorldEffect(Effect cloneSource) : base(cloneSource)
        {
            PrepareParams();
        }

        public WorldEffect(GraphicsDevice graphicsDevice, byte[] effectCode) : base(graphicsDevice, effectCode)
        {
            PrepareParams();
        }

        public WorldEffect(GraphicsDevice graphicsDevice, byte[] effectCode, int index, int count) : base(graphicsDevice, effectCode, index, count)
        {
            PrepareParams();
        }

        protected virtual void PrepareParams()
        {
            PrepareTechniques();
        }

        public void PrepareTechniques()
        {
            var values = Enum.GetValues(TechniqueType);
            IndexedTechniques = new EffectTechnique[values.Length];
            int i = 0;
            foreach (var value in values)
            {
                IndexedTechniques[i++] = Techniques[Enum.GetName(TechniqueType, value)];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTechnique(int type)
        {
            CurrentTechnique = IndexedTechniques[type];
        }
    }
}
