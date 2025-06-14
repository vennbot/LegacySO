
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
using FSO.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSO.SimAntics.NetPlay.SandboxMode
{
    public class BanList
    {
        HashSet<string> BannedHosts;
        string Dir;

        public BanList()
        {
            Dir = FSOEnvironment.UserDir;
            var path = Path.Combine(Dir, "banlist.txt");
            if (!File.Exists(path)) File.Create(path).Close();

            using (var list = File.OpenText(path))
            {
                BannedHosts = new HashSet<string>();
                string line;
                while ((line = list.ReadLine()) != null)
                {
                    BannedHosts.Add(line);
                }
            }
        }

        public bool Contains(string host)
        {
            return BannedHosts.Contains(host.ToLowerInvariant());
        }

        public void Add(string host)
        {
            BannedHosts.Add(host.ToLowerInvariant());
            Write();
        }

        public void Remove(string host)
        {
            BannedHosts.Remove(host.ToLowerInvariant());
            Write();
        }

        public List<string> List()
        {
            return BannedHosts.ToList();
        }

        public void Write()
        {
            using (var list = File.Open(Dir + "banlist.txt", FileMode.Create))
            {
                var writer = new StreamWriter(list);
                foreach (var host in BannedHosts)
                {
                    writer.WriteLine(host);
                }
            }
        }
    }
}
