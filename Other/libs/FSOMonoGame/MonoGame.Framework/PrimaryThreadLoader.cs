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

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Interface used to add an object to be loaded on the primary thread
    /// </summary>
    interface IPrimaryThreadLoaded
    {
        bool Load();
    }

    /// <summary>
    /// Static class that is called before every draw to load resources that need to finish loading on the primary thread
    /// </summary>
    internal static class PrimaryThreadLoader
    {
        private static readonly object ListLockObject = new object();
        private static readonly List<IPrimaryThreadLoaded> NeedToLoad = new List<IPrimaryThreadLoaded>(); 
        private static readonly List<IPrimaryThreadLoaded> RemoveList = new List<IPrimaryThreadLoaded>();
        private static DateTime _lastUpdate = DateTime.UtcNow;

        public static void AddToList(IPrimaryThreadLoaded primaryThreadLoaded)
        {
            lock (ListLockObject)
            {
                NeedToLoad.Add(primaryThreadLoaded);
            }
        }

        public static void RemoveFromList(IPrimaryThreadLoaded primaryThreadLoaded)
        {
            lock (ListLockObject)
            {
                NeedToLoad.Remove(primaryThreadLoaded);
            }
        }

        public static void RemoveFromList(List<IPrimaryThreadLoaded> primaryThreadLoadeds)
        {
            lock (ListLockObject)
            {
                foreach (var primaryThreadLoaded in primaryThreadLoadeds)
                {
                    NeedToLoad.Remove(primaryThreadLoaded);
                }
            }
        }

        public static void Clear()
        {
            lock(ListLockObject)
            {
                NeedToLoad.Clear();
            }
        }

        /// <summary>
        /// Loops through list and loads the item.  If successful, it is removed from the list.
        /// </summary>
        public static void DoLoads()
        {
            if((DateTime.UtcNow - _lastUpdate).Milliseconds < 250) return;

            _lastUpdate = DateTime.UtcNow;
            lock (ListLockObject)
            {
                for (int i = 0; i < NeedToLoad.Count; i++)
                {
                    var primaryThreadLoaded = NeedToLoad[i];
                    if (primaryThreadLoaded.Load())
                    {
                        RemoveList.Add(primaryThreadLoaded);
                    }
                }

                for (int i = 0; i < RemoveList.Count; i++)
                {
                    var primaryThreadLoaded = RemoveList[i];
                    NeedToLoad.Remove(primaryThreadLoaded);
                }

                RemoveList.Clear();
            }
        }
    }
}
