
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
using FSO.Server.DataService.Model;

namespace FSO.Client.Network
{
    public class Topics
    {
        public static ITopic For(MaskedStruct mask, uint entityId)
        {
            return new EntityMaskTopic(mask, entityId);
        }
    }

    public class EntityMaskTopic : ITopic
    {
        public MaskedStruct Mask { get; internal set; }
        public uint EntityId { get; internal set; }

        public EntityMaskTopic(MaskedStruct mask, uint entityId)
        {
            this.Mask = mask;
            this.EntityId = entityId;
        }

        public override bool Equals(object obj)
        {
            if(obj is EntityMaskTopic){
                var cast = (EntityMaskTopic)obj;
                return cast.Mask == Mask && cast.EntityId == EntityId;
            }
            return base.Equals(obj);
        }
    }

    public interface ITopic
    {
    }
}
