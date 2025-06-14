
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
using System;

namespace FSO.LotView
{
    public static class GraphicsModeControl
    {
        private static GlobalGraphicsMode _Mode = GlobalGraphicsMode.Full2D;
        public static event Action<GlobalGraphicsMode> ModeChanged;
        public static GlobalGraphicsMode Mode => _Mode;
        public static bool GlobalTransitionsEnabled => TransitionsEnabled(Mode);

        public static void ChangeMode(GlobalGraphicsMode mode)
        {
            if (!FSOEnvironment.Enable3D) return;
            _Mode = mode;
            ModeChanged?.Invoke(mode);
        }

        public static bool TransitionsEnabled(GlobalGraphicsMode mode)
        {
            return Mode > GlobalGraphicsMode.Full2D;
        }
    }
}
