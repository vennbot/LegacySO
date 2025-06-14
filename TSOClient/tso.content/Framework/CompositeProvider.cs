
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
using System.Collections.Generic;
using FSO.Common.Content;

namespace FSO.Content.Framework
{
    public class CompositeProvider<T> : IContentProvider<T>
    {
        protected IEnumerable<IContentProvider<T>> Providers;

        public CompositeProvider()
        {

        }

        public CompositeProvider(IEnumerable<IContentProvider<T>> providers)
        {
            SetProviders(providers);
        }

        public void SetProviders(IEnumerable<IContentProvider<T>> providers)
        {
            Providers = providers;
        }

        public T Get(ContentID id)
        {
            foreach (var provider in Providers)
            {
                var result = provider.Get(id);
                if (!object.Equals(result, default(T))) return result;
            }
            return default(T);
        }

        public T Get(string name)
        {
            foreach (var provider in Providers)
            {
                var result = provider.Get(name);
                if (!object.Equals(result, default(T))) return result;
            }
            return default(T);
        }

        public T Get(ulong id)
        {
            foreach (var provider in Providers)
            {
                var result = provider.Get(id);
                if (!object.Equals(result, default(T))) return result;
            }
            return default(T);
        }

        public T Get(uint type, uint fileID)
        {
            foreach (var provider in Providers)
            {
                var result = provider.Get(type, fileID);
                if (!object.Equals(result, default(T))) return result;
            }
            return default(T);
        }

        public List<IContentReference<T>> List()
        {
            var total = new List<IContentReference<T>>();
            foreach (var provider in Providers)
            {
                total.AddRange(provider.List());
            }
            return total;
        }

        public List<IContentReference> ListGeneric()
        {
            var total = new List<IContentReference>();
            foreach (var provider in Providers)
            {
                if (provider is TS1SubProvider<T>)
                    total.AddRange(((TS1SubProvider<T>)provider).ListGeneric());
                else
                    total.AddRange(provider.List());
            }
            return total;
        }
    }
}
