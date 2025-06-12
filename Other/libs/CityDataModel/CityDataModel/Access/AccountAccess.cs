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
using System.Text;

namespace CityDataModel.Entities
{
    public class AccountAccess
    {
        private DataAccess Context;

        public AccountAccess(DataAccess context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Creates an account in the DB.
        /// </summary>
        /// <param name="AccountName">The name of the account to create.</param>
        /// <param name="Password">The password of the account to create.</param>
        public void Create(Account account)
        {
            Context.Context.Accounts.InsertOnSubmit(account);
            Context.Context.SubmitChanges();
        }

        /// <summary>
        /// Gets an account object by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Account GetByUsername(string username)
        {
            return Context.Context.Accounts.FirstOrDefault(x => x.AccountName == username.ToUpper());
        }
    }
}
