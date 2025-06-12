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
/*This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
If a copy of the MPL was not distributed with this file, You can obtain one at
http://mozilla.org/MPL/2.0/.

The Original Code is the CityDatamodel.

The Initial Developer of the Original Code is
Mats 'Afr0' Vederhus. All Rights Reserved.

Contributor(s): ______________________________________.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityDataModel.Entities
{
	public class HouseAccess
	{
		private DataAccess Context;

        public HouseAccess(DataAccess context)
        {
            this.Context = context;
        }

		/// <summary>
		/// Attempts to create a house in the DB.
		/// </summary>
		/// <param name="house">A House instance to add to the DB.</param>
		/// <returns>A HouseCreationStatus indicating success or failure.</returns>
		public HouseCreationStatus CreateHouse(House house)
		{
			if (house.Name.Length > 24)
			{
				return HouseCreationStatus.NameTooLong;
			}

			try
			{
				Context.Context.Houses.InsertOnSubmit(house);
				Context.Context.SubmitChanges();
			}
			catch (Exception ex)
			{
				return HouseCreationStatus.GeneralError;
			}

			return HouseCreationStatus.Success;
		}

		/// <summary>
		/// Returns the first house for a specific character GUID.
		/// </summary>
		/// <param name="GUID">A Guid instance for a character.</param>
		/// <returns>IQueryable instance containing the first house for the GUID.</returns>
		public House GetForCharacterGUID(Guid GUID)
		{
			return Context.Context.Characters.FirstOrDefault(x => x.GUID == GUID).HouseHouse;
		}

        /// <summary>
        /// Returns the first house found for the given coordinates, or a default value.
        /// </summary>
        /// <param name="X">X coordinate of house.</param>
        /// <param name="Y">Y coordinate of house.</param>
        /// <returns>The house found, or a House instance with default values.</returns>
        public House GetForPosition(int X, int Y)
        {
            House Ho = Context.Context.Houses.FirstOrDefault(x => x.X == X && x.Y == Y);

            if (Ho != null)
                return Ho;
            else
            {
                Ho = new House();
                Ho.X = X;
                Ho.Y = Y;
                Ho.Description = "You can purchase this lot, it is not owned by anyone.";
                Ho.Name = "TestLot";

                return Ho;
            }
        }
	}

	public enum HouseCreationStatus
	{
		NameAlreadyExisted,
		NameTooLong,
		ExceededHouseLimit,
		Success,
		GeneralError
	}
}
