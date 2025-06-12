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
using System.Collections.Concurrent;

namespace Microsoft.Xna.Framework.Input.Touch
{
    /// <summary>
    /// Stores touches to apply them once a frame for platforms that dispatch touches asynchronously
    /// while user code is running.
    /// </summary>
    internal class TouchQueue
    {
        private readonly ConcurrentQueue<TouchEvent> _queue = new ConcurrentQueue<TouchEvent>();

        public void Enqueue(int id, TouchLocationState state, Vector2 pos, bool isMouse = false)
        {
            _queue.Enqueue(new TouchEvent(id, state, pos, isMouse));
        }

        public void ProcessQueued()
        {
            TouchEvent ev;
            while (_queue.TryDequeue(out ev))                
                TouchPanel.AddEvent(ev.Id, ev.State, ev.Pos, ev.IsMouse);
        }

        private struct TouchEvent
        {
            public readonly int Id;
            public readonly TouchLocationState State;
            public readonly Vector2 Pos;
            public readonly bool IsMouse;

            public TouchEvent(int id, TouchLocationState state, Vector2 pos, bool isMouse)
            {
                Id = id;
                State = state;
                Pos = pos;
                IsMouse = isMouse;
            }
        }

    }
}
