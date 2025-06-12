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
using Mina.Core.Filterchain;
using Mina.Core.Session;

namespace Mina.Example.Haiku
{
    class ToHaikuIoFilter : IoFilterAdapter
    {
        public override void MessageReceived(INextFilter nextFilter, IoSession session, object message)
        {
            List<String> phrases = session.GetAttribute<List<String>>("phrases");

            if (null == phrases)
            {
                phrases = new List<String>();
                session.SetAttribute("phrases", phrases);
            }

            phrases.Add((String)message);

            if (phrases.Count == 3)
            {
                session.RemoveAttribute("phrases");

                base.MessageReceived(nextFilter, session, new Haiku(phrases
                        .ToArray()));
            }
        }
    }
}
