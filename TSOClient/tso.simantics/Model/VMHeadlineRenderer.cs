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
using FSO.LotView;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.SimAntics.Model
{
    public class VMHeadlineRenderer
    {
        protected VMRuntimeHeadline Headline;
        public virtual bool IsMoney { get => false; }

        public VMHeadlineRenderer(VMRuntimeHeadline headline) {
            Headline = headline;
        }

        public virtual Texture2D DrawFrame(World world)
        {
            return null;
        }

        public virtual void Dispose()
        {

        }

        /// <summary>
        /// Returns true if the headline should be killed.
        /// </summary>
        /// <returns></returns>
        public bool Update() {
            Headline.Anim++;
            if (Headline.Duration < 0) return false;
            return (--Headline.Duration <= 0);
        }
    }

    public interface VMHeadlineRendererProvider
    {
        VMHeadlineRenderer Get(VMRuntimeHeadline headline);
    }

    public class VMNullHeadlineProvider : VMHeadlineRendererProvider
    {
        public VMHeadlineRenderer Get(VMRuntimeHeadline headline)
        {
            return new VMHeadlineRenderer(headline);
        }
    }

}
