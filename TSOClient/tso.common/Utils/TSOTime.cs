
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

namespace FSO.Common.Utils
{
    public class TSOTime
    {
        public static Tuple<int,int,int> FromUTC(DateTime time)
        {
            //var count = time.Minute * 60 * 1000 + time.Second * 1000 + time.Millisecond;
            //count *= 8;
            //count %= 1000 * 60 * 24;

            //var hour = count / (1000 * 60);
            //var min = (count / 1000) % 60;
            //var sec = ((count * 60) / 1000) % 60;

            var hour = time.Hour;
            var min = time.Minute;
            var sec = time.Second;
            var ms = time.Millisecond;

            var cycle = (hour % 2 == 1) ? 3600 : 0;
            cycle += min * 60 + sec;
            return new Tuple<int, int, int>(cycle / 300, (cycle % 300) / 5, (cycle % 5)*12 + ((ms * 12) / 1000));
        }
    }
}
