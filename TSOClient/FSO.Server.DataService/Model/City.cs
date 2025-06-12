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
using FSO.Common.DataService.Framework.Attributes;
using System.Collections.Immutable;

namespace FSO.Common.DataService.Model
{
    public class City : AbstractModel
    {
        [Key]
        public uint City_Id { get; set; } //unused

        private ImmutableList<bool> _City_ReservedLotVector;
        public ImmutableList<bool> City_ReservedLotVector {
            get { return _City_ReservedLotVector; }
            set { _City_ReservedLotVector = value; NotifyPropertyChanged("City_ReservedLotVector"); }
        }

        private ImmutableList<bool> _City_OnlineLotVector;
        public ImmutableList<bool> City_OnlineLotVector
        {
            get { return _City_OnlineLotVector; }
            set { _City_OnlineLotVector = value; NotifyPropertyChanged("City_OnlineLotVector"); }
        }

        private ImmutableList<uint> _City_TopTenNeighborhoodsVector;
        public ImmutableList<uint> City_TopTenNeighborhoodsVector
        {
            get { return _City_TopTenNeighborhoodsVector; }
            set { _City_TopTenNeighborhoodsVector = value; NotifyPropertyChanged("City_TopTenNeighborhoodsVector"); }
        }

        //City_LotDBIDByInstanceID map

        private ImmutableList<uint> _City_NeighborhoodsVec;
        public ImmutableList<uint> City_NeighborhoodsVec
        {
            get { return _City_NeighborhoodsVec; }
            set { _City_NeighborhoodsVec = value; NotifyPropertyChanged("City_NeighborhoodsVec"); }
        }

        private ImmutableDictionary<uint, bool> _City_ReservedLotInfo;
        public ImmutableDictionary<uint, bool> City_ReservedLotInfo
        {
            get { return _City_ReservedLotInfo; }
            set { _City_ReservedLotInfo = value; NotifyPropertyChanged("City_ReservedLotInfo"); }
        }

        private ImmutableList<uint> _City_SpotlightsVector;
        public ImmutableList<uint> City_SpotlightsVector
        {
            get { return _City_SpotlightsVector; }
            set { _City_SpotlightsVector = value; NotifyPropertyChanged("City_SpotlightsVector"); }
        }

        //City_LotInstanceIDByDBID map

        private ImmutableList<uint> _City_Top100ListIDs;
        public ImmutableList<uint> City_Top100ListIDs
        {
            get { return _City_Top100ListIDs; }
            set { _City_Top100ListIDs = value; NotifyPropertyChanged("City_Top100ListIDs"); }
        }

        private string _City_NeighJSON;
        public string City_NeighJSON
        {
            get { return _City_NeighJSON; }
            set { _City_NeighJSON = value; NotifyPropertyChanged("City_NeighJSON"); }
        }

    }
}
