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
using System.Reflection;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class BufferedReadStreamTests
    {
        #region Unreadable Stream

        class UnreadableStream : Stream
        {
            Stream _baseStream;

            public UnreadableStream(Stream baseStream)
            {
                _baseStream = baseStream;
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return _baseStream.CanWrite; }
            }

            public override void Flush()
            {
                _baseStream.Flush();
            }

            public override long Length
            {
                get { return _baseStream.Length; }
            }

            public override long Position
            {
                get
                {
                    return _baseStream.Position;
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _baseStream.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _baseStream.Write(buffer, offset, count);
            }
        }

        #endregion

        #region Setup & Teardown

        MemoryStream _baseStream;
        TestHarness.BufferedReadStream _readBuffer;

        [TestFixtureSetUp]
        public void Init()
        {
            _baseStream = new MemoryStream(4096);

            for (int i = 0; i < 2048; i++)
            {
                _baseStream.WriteByte((byte)(i / 256));
                _baseStream.WriteByte((byte)(i % 256));
            }
        }

        [SetUp]
        public void TestInit()
        {
            _baseStream.Position = 0;
        }

        TestHarness.BufferedReadStream GetStandardWrapper()
        {
            return new TestHarness.BufferedReadStream(_baseStream, 256, 1024, false);
        }

        [TearDown]
        public void TestTearDown()
        {
            if (_readBuffer != null)
            {
                _readBuffer.Dispose();
                _readBuffer = null;
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _baseStream.Dispose();
        }

        #endregion

        #region Constructors

        [Test]
        public void Constructor1()
        {
            _readBuffer = new TestHarness.BufferedReadStream(_baseStream);
        }

        [Test]
        public void Constructor2()
        {
            _readBuffer = new TestHarness.BufferedReadStream(_baseStream, true);

            Assert.AreEqual(true, _readBuffer.MinimalRead);
        }

        [Test]
        public void Constructor3()
        {
            _readBuffer = new TestHarness.BufferedReadStream(_baseStream, 64, 1024);

            Assert.AreEqual(1024, _readBuffer.MaxBufferSize);
        }

        [Test]
        public void Constructor4()
        {
            _readBuffer = new TestHarness.BufferedReadStream(_baseStream, 64, 1024, true);

            Assert.AreEqual(true, _readBuffer.MinimalRead);
            Assert.AreEqual(1024, _readBuffer.MaxBufferSize);
        }

        [Test]
        public void ConstructorErrors()
        {
            Assert.Throws<ArgumentNullException>(() => _readBuffer = new TestHarness.BufferedReadStream(null));
            using (var urs = new UnreadableStream(_baseStream))
            {
                Assert.Throws<ArgumentException>(() => new TestHarness.BufferedReadStream(urs));
            }
        }

        [Test]
        public void ConstructorFixMaxSize()
        {
            _readBuffer = new TestHarness.BufferedReadStream(_baseStream, 0, 0);
            Assert.AreEqual(1, _readBuffer.MaxBufferSize);
        }

        #endregion

        #region CloseBaseStream

        [Test]
        public void CloseBaseStream()
        {
            var stream = new MemoryStream(new byte[6]);

            using (var buf = new TestHarness.BufferedReadStream(stream))
            {
                buf.CloseBaseStream = true;
            }

            Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
        }

        #endregion
    }
}
