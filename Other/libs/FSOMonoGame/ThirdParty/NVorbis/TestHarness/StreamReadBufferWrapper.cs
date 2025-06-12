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
using System.IO;

namespace NVorbis
{
    //public partial class StreamReadBuffer
    //{
    //    public long TestingBaseOffset
    //    {
    //        get { return _baseOffset; }
    //        set { _baseOffset = value; }
    //    }
        
    //    public int TestingDiscardCount
    //    {
    //        get { return _discardCount; }
    //        set { _discardCount = value; }
    //    }

    //    public int TestingBufferEndIndex
    //    {
    //        get { return _end; }
    //        set { _end = value; }
    //    }

    //    public byte[] TestingBuffer
    //    {
    //        get { return _data; }
    //        set { _data = value; }
    //    }

    //    public int TestingMaxSize
    //    {
    //        get { return _maxSize; }
    //        set { _maxSize = value; }
    //    }

    //    public long TestingBufferEndOffset
    //    {
    //        get { return BufferEndOffset; }
    //    }

    //    public int TestingReadByte(long offset)
    //    {
    //        return ReadByte(offset);
    //    }

    //    public int TestingEnsureAvailable(long offset, ref int count)
    //    {
    //        return EnsureAvailable(offset, ref count, false);
    //    }

    //    public void TestingCalcBuffer(long offset, int count, out int readStart, out int readEnd)
    //    {
    //        CalcBuffer(offset, count, out readStart, out readEnd);
    //    }

    //    public void TestingEnsureBufferSize(int reqSize, bool copyContents, int copyOffset)
    //    {
    //        EnsureBufferSize(reqSize, copyContents, copyOffset);
    //    }

    //    public int TestingFillBuffer(long offset, int count, int readStart, int readEnd)
    //    {
    //        return FillBuffer(offset, count, readStart, readEnd);
    //    }


    //    public int TestingPrepareStreamForRead(int readCount, long readOffset)
    //    {
    //        return PrepareStreamForRead(readCount, readOffset);
    //    }

    //    public void TestingReadStream(int readStart, int readCount, long readOffset)
    //    {
    //        ReadStream(readStart, readCount, readOffset);
    //    }
    //}
}
namespace TestHarness
{
    //public class StreamReadBuffer : NVorbis.StreamReadBuffer
    //{
    //    public StreamReadBuffer(Stream source, int initialSize, int maxSize, bool minimalRead)
    //        : base(source, initialSize, maxSize, minimalRead)
    //    {
    //        TestingInitialSize = base.Length;
    //    }

    //    public int TestingInitialSize { get; private set; }
    //}
}
