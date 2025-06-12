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
namespace Mina.Core.Session
{
    /// <summary>
    /// Represents the type of idleness of <see cref="IoSession"/>.
    /// </summary>
    public enum IdleStatus
    {
        /// <summary>
        /// Represents the session status that no data is coming from the remote peer.
        /// </summary>
        ReaderIdle,
        /// <summary>
        /// Represents the session status that the session is not writing any data.
        /// </summary>
        WriterIdle,
        /// <summary>
        /// Represents both ReaderIdle and WriterIdle.
        /// </summary>
        BothIdle
    }

    /// <summary>
    /// Provides data for idle events.
    /// </summary>
    public class IdleEventArgs : System.EventArgs
    {
        private readonly IdleStatus _idleStatus;

        /// <summary>
        /// </summary>
        public IdleEventArgs(IdleStatus idleStatus)
        {
            _idleStatus = idleStatus;
        }

        /// <summary>
        /// Gets the idle status.
        /// </summary>
        public IdleStatus IdleStatus
        {
            get { return _idleStatus; }
        }
    }
}
