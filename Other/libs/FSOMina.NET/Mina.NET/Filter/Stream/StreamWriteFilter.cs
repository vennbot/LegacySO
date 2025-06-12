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
using Mina.Core.Buffer;

namespace Mina.Filter.Stream
{
    /// <summary>
    /// Filter implementation which makes it possible to write <see cref="System.IO.Stream"/>
    /// objects directly using <see cref="Core.Session.IoSession.Write(Object)"/>.
    /// <remarks>
    /// When an <see cref="System.IO.Stream"/> is written to a session this filter will read the bytes
    /// from the stream into <see cref="IoBuffer"/> objects and write those buffers
    /// to the next filter.
    /// <para>
    /// This filter will ignore written messages which aren't <see cref="System.IO.Stream"/>
    /// instances. Such messages will be passed to the next filter directly.
    /// </para>
    /// <para>
    /// NOTE: this filter does not close the stream after all data from stream has been written.
    /// </para>
    /// </remarks>
    /// </summary>
    public class StreamWriteFilter : AbstractStreamWriteFilter<System.IO.Stream>
    {
        /// <inheritdoc/>
        protected override IoBuffer GetNextBuffer(System.IO.Stream stream)
        {
            Byte[] bytes = new Byte[WriteBufferSize];

            Int32 off = 0;
            Int32 n = 0;
            while (off < bytes.Length && (n = stream.Read(bytes, off, bytes.Length - off)) > 0)
            {
                off += n;
            }

            if (n <= 0 && off == 0)
                return null;

            return IoBuffer.Wrap(bytes, 0, off);
        }
    }
}
