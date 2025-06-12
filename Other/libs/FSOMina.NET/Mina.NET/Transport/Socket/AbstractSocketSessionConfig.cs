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
using Mina.Core.Session;

namespace Mina.Transport.Socket
{
    abstract class AbstractSocketSessionConfig : AbstractIoSessionConfig, ISocketSessionConfig
    {
        protected override void DoSetAll(IoSessionConfig config)
        {
            ISocketSessionConfig cfg = config as ISocketSessionConfig;
            if (cfg == null)
                return;

            if (cfg.ReceiveBufferSize.HasValue)
                ReceiveBufferSize = cfg.ReceiveBufferSize;
            if (cfg.SendBufferSize.HasValue)
                SendBufferSize = cfg.SendBufferSize;
            if (cfg.ReuseAddress.HasValue)
                ReuseAddress = cfg.ReuseAddress;
            if (cfg.TrafficClass.HasValue)
                TrafficClass = cfg.TrafficClass;
            if (cfg.ExclusiveAddressUse.HasValue)
                ExclusiveAddressUse = cfg.ExclusiveAddressUse;
            if (cfg.KeepAlive.HasValue)
                KeepAlive = cfg.KeepAlive;
            if (cfg.OobInline.HasValue)
                OobInline = cfg.OobInline;
            if (cfg.NoDelay.HasValue)
                NoDelay = cfg.NoDelay;
            if (cfg.SoLinger.HasValue)
                SoLinger = cfg.SoLinger;
        }

        public abstract Int32? ReceiveBufferSize { get; set; }

        public abstract Int32? SendBufferSize { get; set; }

        public abstract Boolean? NoDelay { get; set; }

        public abstract Int32? SoLinger { get; set; }

        public abstract Boolean? ExclusiveAddressUse { get; set; }

        public abstract Boolean? ReuseAddress { get; set; }

        public abstract Int32? TrafficClass { get; set; }

        public abstract Boolean? KeepAlive { get; set; }

        public abstract Boolean? OobInline { get; set; }
    }
}
