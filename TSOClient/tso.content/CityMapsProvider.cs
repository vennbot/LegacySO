
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
using FSO.Common.Content;
using FSO.Content.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace FSO.Content
{
    public class CityMapsProvider : IContentProvider<CityMap>
    {
        private ConcurrentDictionary<int, CityMap> Cache;
        private Dictionary<int, string> DirCache;
        private Content Content;
        
        public CityMapsProvider(Content content)
        {
            this.Content = content;
        }

        public void Init()
        {
            DirCache = new Dictionary<int, string>();
            Cache = new ConcurrentDictionary<int, CityMap>();

            var dir = Content.GetPath("cities");
            foreach (var map in Directory.GetDirectories(dir))
            {
                var id = int.Parse(Path.GetFileName(map).Replace("city_", ""));
                DirCache.Add(id, map);
            }

            dir = Path.Combine(FSOEnvironment.ContentDir, "Cities/");
            foreach (var map in Directory.GetDirectories(dir))
            {
                var id = int.Parse(Path.GetFileName(map).Replace("city_", ""));
                DirCache.Add(id, map);
            }
        }

        public CityMap Get(string id)
        {
            return Get(ulong.Parse(id));
        }

        public CityMap Get(ulong id)
        {
            CityMap result;
            if (Cache.TryGetValue((int)id, out result))
            {
                return result;
            } else
            {
                return Cache.GetOrAdd((int)id, new CityMap(DirCache[(int)id]));
            }
        }

        public CityMap Get(uint type, uint fileID)
        {
            throw new NotImplementedException();
        }

        public List<IContentReference<CityMap>> List()
        {
            throw new NotImplementedException();
        }

        public CityMap Get(ContentID id)
        {
            throw new NotImplementedException();
        }
    }
}
