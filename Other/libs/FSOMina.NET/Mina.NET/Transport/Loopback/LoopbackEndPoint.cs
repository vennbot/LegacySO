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
using System.Net;

namespace Mina.Transport.Loopback
{
    /// <summary>
    /// An endpoint which represents loopback port number.
    /// </summary>
    public class LoopbackEndPoint : EndPoint, IComparable<LoopbackEndPoint>
    {
        private readonly Int32 _port;

        /// <summary>
        /// Creates a new instance with the specifid port number.
        /// </summary>
        public LoopbackEndPoint(Int32 port)
        {
            _port = port;
        }

        /// <summary>
        /// Gets the port number.
        /// </summary>
        public Int32 Port
        {
            get { return _port; }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(LoopbackEndPoint other)
        {
            return this._port.CompareTo(other._port);
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return _port.GetHashCode();
        }

        /// <inheritdoc/>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            LoopbackEndPoint other = obj as LoopbackEndPoint;
            return obj != null && this._port == other._port;
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return _port >= 0 ? ("vm:server:" + _port) : ("vm:client:" + -_port);
        }
    }
}
