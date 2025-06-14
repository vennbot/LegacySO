
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
using Dapper;
using System.Collections.Generic;
using System.Data.Common;

namespace FSO.Server.Database.DA.Utils
{
    public static class BufferedInsert
    {
        public static void ExecuteBufferedInsert(this DbConnection connection, string query, IEnumerable<object> param, int batches)
        {
            var buffer = new List<object>();
            var enumerator = param.GetEnumerator();

            while (enumerator.MoveNext())
            {
                buffer.Add(enumerator.Current);

                if(buffer.Count >= batches)
                {
                    connection.Execute(query, buffer);
                    buffer.Clear();
                }
            }

            if(buffer.Count > 0)
            {
                connection.Execute(query, buffer);
            }
        }
    }
}
