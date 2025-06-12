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

namespace FSO.Common
{
    /// <summary>
    /// TODO: apply a time delta to sync with server time. right now we assume client time is correct.
    /// </summary>
    public class ClientEpoch
    {
        public static uint Now
        {
            get
            {
                uint epoch = (uint)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                return epoch;
            }
        }

        public static uint FromDate(DateTime time)
        {
            return (uint)(time.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime ToDate(uint time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(time);
        }

        public static string HMSRemaining(uint date)
        {
            TimeSpan span = (ToDate(date) - ToDate(ClientEpoch.Now));

            return String.Format("{0} hours, {1} minutes and {2} seconds", (int)span.TotalHours, span.Minutes, span.Seconds);
        }

        public static string DHMRemaining(uint date)
        {
            if (date == uint.MaxValue) return "Permanent";
            TimeSpan span = (ToDate(date) - ToDate(ClientEpoch.Now));

            return String.Format("{0} days, {1} hours and {2} minutes", (int)span.TotalDays, span.Hours, span.Minutes);
        }

        public static uint Default
        {
            get { return 0; }
        }
    }
}
