
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
using FSO.Server.Database.DA.Updates;

namespace FSO.Server.Api.Core.Models
{
    public class UpdateGenerationStatus
    {
        public int TaskID;
        public UpdateCreateModel Request;
        public UpdateGenerationStatusCode Code;
        public float EstimatedProgress;

        public DbUpdate Result;
        public string Failure;

        public UpdateGenerationStatus(int taskID, UpdateCreateModel request)
        {
            TaskID = taskID;
            Request = request;
        }

        public void UpdateStatus(UpdateGenerationStatusCode code, float progress)
        {
            Code = code;
            EstimatedProgress = progress;
        }

        public void UpdateStatus(UpdateGenerationStatusCode code)
        {
            UpdateStatus(code, ((float)code - 1) / ((float)UpdateGenerationStatusCode.SUCCESS - 1));
        }

        public void SetResult(DbUpdate result)
        {
            UpdateStatus(UpdateGenerationStatusCode.SUCCESS);
            Result = result;
        }

        public void SetFailure(string failure)
        {
            UpdateStatus(UpdateGenerationStatusCode.FAILURE, 0);
            Failure = failure;
        }
    }

    public enum UpdateGenerationStatusCode
    {
        FAILURE = 0,

        PREPARING = 1,
        DOWNLOADING_CLIENT,
        DOWNLOADING_SERVER,
        DOWNLOADING_CLIENT_ADDON,
        DOWNLOADING_SERVER_ADDON,
        EXTRACTING_CLIENT,
        EXTRACTING_CLIENT_ADDON,
        BUILDING_DIFF,
        BUILDING_INCREMENTAL_UPDATE,
        BUILDING_CLIENT,
        PUBLISHING_CLIENT,

        EXTRACTING_SERVER,
        EXTRACTING_SERVER_ADDON,
        BUILDING_SERVER,
        PUBLISHING_SERVER,

        SCHEDULING_UPDATE,
        SUCCESS
    }
}
