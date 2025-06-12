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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TSOClient.Code.Rendering.Lot.Model;

namespace TSOClient.Code.Rendering.Lot.Components
{
    public abstract class House2DComponent
    {
        /// <summary>
        /// Position of fixed tile objects on the tile space
        /// </summary>
        public Point Position;

        /// <summary>
        /// Height of this component, used to calculate damage region
        /// </summary>
        public abstract int Height { get; }
        
        public virtual void OnZoomChanged(HouseRenderState state)
        {
        }

        public virtual void OnRotationChanged(HouseRenderState state)
        {
        }

        public virtual void OnScrollChange(HouseRenderState state)
        {
        }

        public abstract void Draw(HouseRenderState state, HouseBatch batch);
    }
}
