
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
using FSO.Common.Rendering.Framework.Model;
using Microsoft.Xna.Framework;

namespace FSO.Client.UI.Panels.LotControls
{
    public abstract class UICustomLotControl
    {
        public UILotControlModifiers Modifiers { get; set; }
        public Point MousePosition { get; set; }

        public abstract void MouseDown(UpdateState state);
        public abstract void MouseUp(UpdateState state);
        public abstract void Update(UpdateState state, bool scrolled);

        public abstract void Release();
    }

    [Flags]
    public enum UILotControlModifiers
    {
        SHIFT = 1,
        CTRL = 2
    }

    public static class UILotControlModifierExtensions
    {
        public static bool IsSet(this UILotControlModifiers mode, UILotControlModifiers flag)
        {
            return (mode & flag) == flag;
        }
    }
}
