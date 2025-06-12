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

namespace NVorbis.OpenTKSupport
{
    public class NullLogger : ILogger
    {
        public static readonly ILogger Default = new NullLogger();

        public void Log(LogEventBoolean eventType, Func<bool> context) { }
        public void Log(LogEventBoolean eventType, bool context) { }
        public void Log(LogEventSingle eventType, Func<float> context) { }
        public void Log(LogEventSingle eventType, float context) { }
        public void Log(LogEvent eventType, OggStream stream) { }
    }

    public abstract class LoggerBase : ILogger
    {
        public void Log(LogEventBoolean eventType, Func<bool> context) { Log(eventType, context()); }
        public abstract void Log(LogEventBoolean eventType, bool context);

        public void Log(LogEventSingle eventType, Func<float> context) { Log(eventType, context()); }
        public abstract void Log(LogEventSingle eventType, float context);

        public abstract void Log(LogEvent eventType, OggStream stream);
    }

    public interface ILogger
    {
        void Log(LogEventBoolean eventType, Func<bool> context);
        void Log(LogEventBoolean eventType, bool context);
        void Log(LogEventSingle eventType, Func<float> context);
        void Log(LogEventSingle eventType, float context);
        void Log(LogEvent eventType, OggStream stream);
    }

    public enum LogEventBoolean
    {
        IsOpenAlSoft,
        XRamSupport,
        EfxSupport,
    }
    public enum LogEventSingle
    {
        MemoryUsage,
    }
    public enum LogEvent
    {
        BeginPrepare,
        EndPrepare,
        Play,
        Stop,
        Pause,
        Resume,
        Empty,
        NewPacket,
        LastPacket,
        BufferUnderrun
    }
}
