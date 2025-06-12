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
using Microsoft.Xna.Framework.Input.Touch;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    [TestFixture]
    class TouchLocationTest
    {
        [Test]
        [Description("AgeState is used to make touches that were placed and removed on the same frame appear over multiple frames when no actual update happens on the second frame. When it does this it should copy its details so they can be fetched from the previous state")]
        public void AgeStateUpdatesPreviousDetails([Values(true, false)] bool isSameFrameReleased)
        {
            var touch = new TouchLocation(1, TouchLocationState.Pressed, new Vector2(1, 2), TimeSpan.FromSeconds(1));

            if (isSameFrameReleased)
                touch.SameFrameReleased = true;

            touch.AgeState();

            TouchLocation previous;
            Assert.True(touch.TryGetPreviousLocation(out previous));

            Assert.AreEqual(TouchLocationState.Pressed, previous.State);
            Assert.AreEqual(touch.Id, previous.Id);
            Assert.AreEqual(touch.Position, previous.Position);
            Assert.AreEqual(touch.Timestamp, previous.Timestamp);
        }
    }
}
