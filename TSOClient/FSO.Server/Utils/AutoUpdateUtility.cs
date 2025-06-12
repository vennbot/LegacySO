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
using FSO.Server.Common;
using FSO.Server.Database.DA;
using FSO.Server.Database.DA.Updates;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSO.Server.Utils
{
    public static class AutoUpdateUtility
    {
        public static bool QueueUpdateIfRequired(IKernel kernel, string branch)
        {
            var version = ServerVersion.Get();
            var factory = kernel.Get<IDAFactory>();
            using (var da = factory.Get())
            {
                //first - did we install an update last time?
                if (File.Exists("scheduledUpdate/complete.txt"))
                {
                    try
                    {
                        var completeInfo = File.ReadAllText("scheduledUpdate/complete.txt");
                        //var branchItem = da.Updates.GetBranch(branch);
                        //da.Updates.UpdateBranchLatestDeployed(branchItem.branch_id, int.Parse(completeInfo));
                        try
                        {
                            Directory.Delete("scheduledUpdate/", true);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    catch
                    {
                        //invalid complete.txt?
                    }
                }

                var updates = da.Updates.GetPublishableByBranchName(branch).ToList();
                if (updates.Count != 0) { 

                    //prepare update for watchdog to apply
                    var ordered = updates.OrderBy(x => x.publish_date); //already ordered by add date, so same publish date will publish in order added to db.

                    foreach (var update in ordered)
                    {
                        da.Updates.MarkUpdatePublished(update.update_id);
                    }

                    var last = ordered.Last();
                    da.Updates.UpdateBranchLatestDeployed(last.branch_id, last.update_id);
                }

                //does the latest update for our branch match our current update id?

                var latest = da.Updates.GetBranch(branch)?.current_dist_id;
                if (latest != version.UpdateID)
                {
                    var published = da.Updates.GetRecentUpdatesForBranchByName(branch, 100);
                    var path = FindPath(published.ToList(), version.UpdateID, latest);
                    if (path == null)
                    {
                        //oops
                        return false;
                    }
                    string updateFile = "UPDATE\n";
                    foreach (var update in path)
                    {
                        updateFile += update.server_zip + "\n";
                    }
                    updateFile += latest;
                    Directory.CreateDirectory("scheduledUpdate/");
                    File.WriteAllText("scheduledUpdate/update.txt", updateFile);
                    return true;
                }

            }
            return false;
        }

        public static List<DbUpdate> FindPath(List<DbUpdate> updates, int? current, int? target)
        {
            var to = updates.FirstOrDefault(x => x.update_id == target);
            if (to == null) return null; //cannot find path, fallback on what the server told us before we looked for the delta
            var from = updates.FirstOrDefault(x => x.update_id == current);
            if (from != null)
            {
                //search for route from "to" to "from". recursive search - we then return the updates in order of application
                var follow = to;
                var result = new List<DbUpdate>();
                while (follow != null)
                {
                    if (follow == from) return result; //we got here with incremental updates.
                    result.Insert(0, follow);
                    if (follow.incremental_zip == null) break;
                    follow = (follow.last_update_id == null) ? null : updates.FirstOrDefault(x => x.update_id == follow.last_update_id.Value);
                }
            }
            //we couldn't find a path to our current version. find a path to any update that has a full zip.
            {
                var follow = to;
                var result = new List<DbUpdate>();
                while (follow != null)
                {
                    result.Insert(0, follow);
                    if (follow.full_zip != null) return result; //we found a full zip
                    follow = (follow.last_update_id == null) ? null : updates.FirstOrDefault(x => x.update_id == follow.last_update_id.Value);
                }
            }

            return null; //no clue what to do here, we could not find a full zip to build from. this is fatal.
        }
    }
}
