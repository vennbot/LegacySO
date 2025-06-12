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
using System.IO.Ports;
using System.Net;

namespace Mina.Transport.Serial
{
    /// <summary>
    /// An endpoint for a serial port communication.
    /// </summary>
    public class SerialEndPoint : EndPoint
    {
        private readonly String _portName;
        private readonly Int32 _baudRate;
        private readonly Int32 _dataBits;
        private readonly StopBits _stopBits;
        private readonly Parity _parity;

        /// <summary>
        /// Instantiates.
        /// </summary>
        /// <param name="portName">the port name</param>
        /// <param name="baudRate">the baud rate</param>
        public SerialEndPoint(String portName, Int32 baudRate)
            : this(portName, baudRate, Parity.None, 8, StopBits.One)
        { }

        /// <summary>
        /// Instantiates.
        /// </summary>
        /// <param name="portName">the port name</param>
        /// <param name="baudRate">the baud rate</param>
        /// <param name="parity">the <see cref="Parity"/></param>
        /// <param name="dataBits">the data bits</param>
        /// <param name="stopBits">the <see cref="StopBits"/></param>
        public SerialEndPoint(String portName, Int32 baudRate,
            Parity parity, Int32 dataBits, StopBits stopBits)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
        }

        /// <summary>
        /// Gets the serial port name.
        /// </summary>
        public String PortName
        {
            get { return _portName; }
        }

        /// <summary>
        /// Gets the baud rate.
        /// </summary>
        public Int32 BaudRate
        {
            get { return _baudRate; }
        }

        /// <summary>
        /// Gets the parity.
        /// </summary>
        public Parity Parity
        {
            get { return _parity; }
        }

        /// <summary>
        /// Gets the data bits.
        /// </summary>
        public Int32 DataBits
        {
            get { return _dataBits; }
        }

        /// <summary>
        /// Gets the stop bits.
        /// </summary>
        public StopBits StopBits
        {
            get { return _stopBits; }
        }
    }
}
#endif
