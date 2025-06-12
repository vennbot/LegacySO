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

namespace FSO.Common.Utils.Cache
{
    public class CacheKey : IEqualityComparer<CacheKey>, IEquatable<CacheKey>
    {
        public static CacheKey Root = CacheKey.For();

        public string[] Components { get; internal set; }

        protected CacheKey(string[] components)
        {
            this.Components = components;
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public bool Equals(CacheKey x, CacheKey y)
        {
            return x.Components.SequenceEqual(y.Components);
        }

        public bool Equals(CacheKey other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(CacheKey obj)
        {
            int hashcode = 0;
            foreach (string value in obj.Components)
            {
                if (value != null)
                    hashcode += value.GetHashCode();
            }
            return hashcode;
        }

        public static CacheKey For(params object[] components)
        {
            return new CacheKey(components.Select(x => "" + x).ToArray());
        }

        public static CacheKey Combine(CacheKey domain, params object[] components)
        {
            var newComponents = new string[domain.Components.Length+components.Length];
            Array.Copy(domain.Components, newComponents, domain.Components.Length);
            Array.Copy(components.Select(x => "" + x).ToArray(), 0, newComponents, domain.Components.Length, components.Length);
            return new CacheKey(newComponents);
        }

    }
}
