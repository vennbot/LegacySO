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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Provides helper methods to make it easier
    /// to safely raise events.
    /// </summary>
    internal static class EventHelpers
    {
        /// <summary>
        /// Safely raises an event by storing a copy of the event's delegate
        /// in the <paramref name="handler"/> parameter and checking it for
        /// null before invoking it.
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="handler"><see cref="EventHandler{TEventArgs}"/> to be invoked</param>
        /// <param name="e">The <typeparamref name="TEventArgs"/> passed to <see cref="EventHandler{TEventArgs}"/></param>
        internal static void Raise<TEventArgs>(object sender, EventHandler<TEventArgs> handler, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        /// Safely raises an event by storing a copy of the event's delegate
        /// in the <paramref name="handler"/> parameter and checking it for
        /// null before invoking it.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="handler"><see cref="EventHandler"/> to be invoked</param>
        /// <param name="e">The <see cref="EventArgs"/> passed to <see cref="EventHandler"/></param>
        internal static void Raise(object sender, EventHandler handler, EventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }
    }
}
