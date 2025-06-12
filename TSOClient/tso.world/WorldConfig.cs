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
using FSO.Common;
using FSO.LotView.Model;

namespace FSO.LotView
{
    public class WorldConfig
    {
        public static WorldConfig Current = new WorldConfig();

        //(off, advanced, +3d wall, ultra)
        public int LightingMode;

        public bool AdvancedLighting
        {
            get
            {
                return (LightingMode > 0);
            }
        }
        public bool Shadow3D
        {
            get
            {
                return (LightingMode > 1);
            }
        }
        public bool UltraLighting
        {
            get
            {
                return (LightingMode > 2);
            }
        }
        public bool Weather = true;
        public int SurroundingLots = 0;
        public bool SmoothZoom
        {
            get
            {
                return _EnableTransitions;
            }
            set
            {

            }
        }
        public int AA = 0;
        public bool Directional = true;
        public bool Complex = false;

        private bool _EnableTransitions = false;
        public bool EnableTransitions
        {
            get
            {
                return _EnableTransitions;
            }
            set
            {
                _EnableTransitions = FSOEnvironment.Enable3D && value;
            }
        }

        public GlobalGraphicsMode Mode = GlobalGraphicsMode.Hybrid2D;

        public int PassOffset
        {
            get {
                return (AdvancedLighting)?1:0;
            }
        }

        public int DirPassOffset
        {
            get
            {
                return (AdvancedLighting) ? ((Directional)?1:1) : 0;
            }
        }
    }
}
