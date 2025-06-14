
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

namespace FSO.SimAntics.Model.TSOPlatform
{
    /// <summary>
    /// An interface that allows components outwith SimAntics to provide names for avatars not present within the lot.
    /// </summary>
    public interface IVMAvatarNameCache
    {
        string GetNameForID(VM vm, uint persistID);

        /// <summary>
        /// Called to cache an avatar in. This is asynchronous - so it should be called before the user has any chance to do any action that requires the name.
        /// Ideal call times: When we join the lot (cache all roommates), when a roommate changes (cache the new roommate).
        /// </summary>
        /// <param name="persistID">The Persist ID for the avatar whose name we want to cache.</param>
        bool Precache(VM vm, uint persistID);
    }

    public class VMBasicAvatarNameCache : IVMAvatarNameCache
    {
        protected Dictionary<uint, string> AvatarNames = new Dictionary<uint, string>();

        public virtual string GetNameForID(VM vm, uint persistID)
        {
            if (persistID == 0) return "";
            string name;
            if (AvatarNames.TryGetValue(persistID, out name))
                return name;
            if (Precache(vm, persistID))
            {
                if (AvatarNames.TryGetValue(persistID, out name))
                    return name;
            }
            return "(offline user)";
        }

        public virtual bool Precache(VM vm, uint persistID)
        {
            //very simple implementation. if the sim is in the lot, cache their name
            var ava = vm.GetAvatarByPersist(persistID);
            if (ava != null)
            {
                AvatarNames[persistID] = ava.Name;
                return true;
            }
            return false;
        }
    }
}
