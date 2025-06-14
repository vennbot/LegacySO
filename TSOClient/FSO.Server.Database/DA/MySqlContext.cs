
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
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

namespace FSO.Server.Database.DA
{
    public class MySqlContext : ISqlContext, IDisposable
    {
        private readonly string _connectionString;
        private DbConnection _connection;

        public MySqlContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new MySqlConnection(_connectionString);

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public void Flush()
        {
            Dispose();
        }
    }
}
