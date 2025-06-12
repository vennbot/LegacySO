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
#if !UNITY
using System;
using Mina.Core.Session;

namespace Mina.Transport.Serial
{
    /// <summary>
    /// The default configuration for a serial session 
    /// </summary>
    class DefaultSerialSessionConfig : AbstractIoSessionConfig, ISerialSessionConfig
    {
        private Int32 _readTimeout;
        private Int32 _writeBufferSize;
        private Int32 _receivedBytesThreshold;

        public DefaultSerialSessionConfig()
        {
            // reset configs
            ReadBufferSize = 0;
            WriteTimeout = 0;
        }

        protected override void DoSetAll(IoSessionConfig config)
        {
            ISerialSessionConfig cfg = config as ISerialSessionConfig;
            if (cfg != null)
            {
                ReadTimeout = cfg.ReadTimeout;
                WriteBufferSize = cfg.WriteBufferSize;
                ReceivedBytesThreshold = cfg.ReceivedBytesThreshold;
            }
        }

        public Int32 ReadTimeout
        {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }

        public Int32 WriteBufferSize
        {
            get { return _writeBufferSize; }
            set { _writeBufferSize = value; }
        }

        public Int32 ReceivedBytesThreshold
        {
            get { return _receivedBytesThreshold; }
            set { _receivedBytesThreshold = value; }
        }
    }
}
#endif
