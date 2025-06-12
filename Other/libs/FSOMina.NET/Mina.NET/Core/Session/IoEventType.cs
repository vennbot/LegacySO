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

namespace Mina.Core.Session
{
    /// <summary>
    /// Represents the type of I/O events and requests.
    /// It is usually used by internal components to store I/O events.
    /// </summary>
    [Flags]
    public enum IoEventType
    {
        None = 0,
        SessionCreated = 0x1,
        SessionOpened = 0x2,
        SessionClosed = 0x4,
        MessageReceived = 0x8,
        MessageSent = 0x10,
        SessionIdle = 0x20,
        ExceptionCaught = 0x40,
        Write = 0x80,
        Close = 0x100
    }
}
