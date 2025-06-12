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
using FSO.Common.Rendering.Framework.IO;
using FSO.Common.Rendering.Framework.Model;
using Microsoft.Xna.Framework;
using System;

namespace FSO.Client.UI.Framework
{
    public class DoubleClick
    {
        private const int MOUSE_DRIFT_TOLERANCE = 10;

        private long LastClick;
        private Point LastMousePosition;

        public bool TryDoubleClick(UIMouseEventType type, UpdateState update)
        {
            if(type == UIMouseEventType.MouseUp)
            {
                var now = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                if (now - LastClick < 500 && IsMouseClose(LastMousePosition, update.MouseState.Position))
                {
                    LastClick = now;
                    LastMousePosition = update.MouseState.Position;
                    return true;
                }
                LastClick = now;
                LastMousePosition = update.MouseState.Position;
            }

            return false;
        }

        private bool IsMouseClose(Point previous, Point current)
        {
            return Math.Abs(previous.X - current.X) < MOUSE_DRIFT_TOLERANCE &&
                    Math.Abs(previous.Y - current.Y) < MOUSE_DRIFT_TOLERANCE;
        }
    }
}
