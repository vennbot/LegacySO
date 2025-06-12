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

namespace FSO.Client.UI.Framework
{
    public abstract class GameScreen : UIScreen
    {
        private bool m_Scale800x600;

        public bool Scale800x600
        {
            get { return m_Scale800x600; }
            set
            {
                m_Scale800x600 = value;
                if (value)
                {
                    ScaleX = ScaleY = ScreenWidth / 800.0f;
                }
                else
                {
                    ScaleX = ScaleY = 1.0f;
                }
            }
        }

        public override Rectangle GetBounds()
        {
            if (m_Scale800x600)
            {
                return new Rectangle(0, 0, 800, 600);
            }
            return base.GetBounds();
        }
    }
}
