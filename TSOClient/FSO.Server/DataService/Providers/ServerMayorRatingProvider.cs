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
using FSO.Common.DataService.Framework;
using FSO.Common.DataService.Model;
using FSO.Server.Database.DA;
using Ninject;
using NLog;

namespace FSO.Server.DataService.Providers
{
    public class ServerMayorRatingProvider : LazyDataServiceProvider<uint, MayorRating>
    {
        private static Logger LOG = LogManager.GetCurrentClassLogger();
        private int ShardId;
        private IDAFactory DAFactory;

        public ServerMayorRatingProvider([Named("ShardId")] int shardId, IDAFactory factory)
        {
            this.ShardId = shardId;
            this.DAFactory = factory;
        }

        protected override MayorRating LazyLoad(uint key, MayorRating oldVal)
        {
            using (var db = DAFactory.Get())
            {
                var rating = db.Elections.GetRating(key);
                if (rating == null) { return null; }

                return new MayorRating()
                {
                    MayorRating_Comment = rating.comment,
                    MayorRating_Date = rating.date,
                    MayorRating_FromAvatar = (rating.anonymous > 0) ? 0 : rating.from_avatar_id,
                    MayorRating_ToAvatar = rating.to_avatar_id,
                    MayorRating_HalfStars = rating.rating,
                    MayorRating_Neighborhood = rating.neighborhood,
                    Id = key
                };
            }
        }
    }
}
