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
using FSO.Server.Database.DA;
using FSO.Server.Database.Management;
using NLog;
using System;

namespace FSO.Server
{
    /// <summary>
    /// This tool will install and update the SQL database.
    /// 
    /// It does this by reading the manifest.json file from the Scripts folder and compares it
    /// to whats inside the fso_db_changes table.
    /// 
    /// </summary>
    public class ToolInitDatabase : ITool
    {
        private static Logger LOG = LogManager.GetCurrentClassLogger();
        private IDAFactory DAFactory;

        public ToolInitDatabase(DatabaseInitOptions options, IDAFactory factory)
        {
            this.DAFactory = factory;
        }

        public int Run()
        {
            Console.WriteLine("Starting database init");

            using (var da = (SqlDA)DAFactory.Get())
            {
                var changeTool = new DbChangeTool(da.Context);
                var changes = changeTool.GetChanges();

                foreach(var change in changes)
                {
                    if(change.Status == DbChangeScriptStatus.MODIFIED && change.Idempotent == false)
                    {
                        Console.WriteLine(change.Status + " - " + change.ScriptFilename + " (Cant update, fix manually)");
                    }
                    else
                    {
                        Console.WriteLine(change.Status + " - " + change.ScriptFilename);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Apply changes (y|n)? Make sure you have backed up your database first");

                var input = Console.ReadLine().Trim();
                if (input.StartsWith("y") || input.StartsWith("r"))
                {
                    //Repair just updates fso_db_changes to latest
                    var repair = input.StartsWith("r");

                    Console.WriteLine("Applying changes");

                    foreach (var change in changes)
                    {
                        if(change.Status == DbChangeScriptStatus.FORCE_REINSTALL ||
                            change.Status == DbChangeScriptStatus.NOT_INSTALLED ||
                            (change.Status == DbChangeScriptStatus.MODIFIED && change.Idempotent))
                        {
                            try {
                                changeTool.ApplyChange(change, repair);
                            }catch(DbMigrateException e)
                            {
                                Console.Error.WriteLine("Error applying change: " + change.ScriptFilename);
                                Console.Error.WriteLine("\"" + e.Message + "\"");
                                Console.WriteLine("Would you like to continue? (y|n)?");
                                input = Console.ReadLine().Trim();
                                if (!input.StartsWith("y"))
                                {
                                    return -1;
                                }
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("No changes applied");
                }
            }
            return 0;
        }
    }
}
