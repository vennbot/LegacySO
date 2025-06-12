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
/* Copyright (c) 2010 Michael Lidgren

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Diagnostics;

namespace Lidgren.Network
{
	public partial class NetPeer
	{
		[Conditional("DEBUG")]
		internal void LogVerbose(string message)
		{
#if __ANDROID__
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Verbose, "", message);
#endif
			if (m_configuration.IsMessageTypeEnabled(NetIncomingMessageType.VerboseDebugMessage))
				ReleaseMessage(CreateIncomingMessage(NetIncomingMessageType.VerboseDebugMessage, message));
		}

		[Conditional("DEBUG")]
		internal void LogDebug(string message)
		{
#if __ANDROID__
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Debug, "", message);
#endif
			if (m_configuration.IsMessageTypeEnabled(NetIncomingMessageType.DebugMessage))
				ReleaseMessage(CreateIncomingMessage(NetIncomingMessageType.DebugMessage, message));
		}

		internal void LogWarning(string message)
		{
#if __ANDROID__
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Warn, "", message);
#endif
			if (m_configuration.IsMessageTypeEnabled(NetIncomingMessageType.WarningMessage))
				ReleaseMessage(CreateIncomingMessage(NetIncomingMessageType.WarningMessage, message));
		}

		internal void LogError(string message)
		{
#if __ANDROID__
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "", message);
#endif
			if (m_configuration.IsMessageTypeEnabled(NetIncomingMessageType.ErrorMessage))
				ReleaseMessage(CreateIncomingMessage(NetIncomingMessageType.ErrorMessage, message));
		}
	}
}
