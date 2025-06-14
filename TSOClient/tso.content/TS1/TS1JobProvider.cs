
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
using FSO.Content.Framework;
using FSO.Files.Formats.IFF;
using FSO.Files.Formats.IFF.Chunks;
using System.Linq;

namespace FSO.Content.TS1
{
    public class TS1JobProvider
    {
        public IffFile JobResource;
        public int NumJobs;
        
        public TS1JobProvider(TS1Provider provider)
        {
            JobResource = (IffFile)provider.Get("work.iff");
            NumJobs = JobResource.List<CARR>().Count-1; //loads all jobs
        }

        public short GetJobData(ushort jobID, int jobLevel, int data)
        {
            return (short)(JobResource.Get<CARR>(jobID)?.GetJobData(jobLevel, data) ?? 0);
        }

        public CARR GetJob(ushort jobID)
        {
            return JobResource.Get<CARR>(jobID);
        }

        public short SetToNext(short current)
        {
            return (short)(JobResource.List<CARR>().FirstOrDefault(x => x.ChunkID > current)?.ChunkID ?? -1);
        }

        public string JobOffer(short jobID, int jobLevel)
        {
            //TODO: use STR#
            var job = JobResource.Get<CARR>((ushort)jobID);
            return (job?.Name ?? "(unknown)") + " career track for a " + job?.JobLevels[jobLevel].JobName + ".";
        }

        public STR JobStrings(short jobID)
        {
            //TODO: use STR#
            return JobResource.Get<STR>((ushort)jobID);
        }

        public JobLevel GetJobLevel(short jobID, int jobLevel)
        {
            var job = JobResource.Get<CARR>((ushort)jobID);
            return job?.JobLevels[jobLevel];
        }
    }
}
