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
using MySql.Data.MySqlClient;
using LoginDataModel.Entities;

namespace LoginDataModel
{
    /// <summary>
    /// This is the data access layer. Whenever code wants to get/put/search something
    /// in the database, it should use this class like this:
    /// 
    /// using(var model = DataAccess.Get()){
    ///     model.(whatever i want)
    /// }
    /// </summary>
    public class DataAccess : IDisposable
    {
        public static string ConnectionString;

        public static DataAccess Get()
        {
            var db = new DB(new MySqlConnection(ConnectionString));
            return new DataAccess(db);
        }


        private DB _Model;
        private AccountAccess _Accounts;
        private CharacterAccess _Character;

        public DataAccess(DB db){
            this._Model = db;
        }


        public AccountAccess Accounts
        {
            get
            {
                if (_Accounts == null)
                {
                    _Accounts = new AccountAccess(this);
                }
                return _Accounts;
            }
        }

        public CharacterAccess Characters
        {
            get
            {
                if (_Character == null)
                {
                    _Character = new CharacterAccess(this);
                }
                return _Character;
            }
        }

        public DB Context {
            get
            {
                return _Model;
            }
        }

        #region IDisposable Members
        public void Dispose()
        {
            try
            {
                _Model.SubmitChanges();
                _Model.Dispose();
            }
            catch (Exception e)
            {
                Logger.Log("Unhandled exception in LoginDataModel.DataAccess.Dispose:\n" + e.ToString(), LogLevel.error);
                return;
            }
        }
        #endregion
    }
}
