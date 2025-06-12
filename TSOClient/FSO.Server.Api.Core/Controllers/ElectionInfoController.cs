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
using FSO.Server.Api.Core.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using FSO.Server.Database.DA.Elections;

namespace FSO.Server.Api.Core.Controllers
{
    [EnableCors]
    [ApiController]
    public class ElectionController : ControllerBase
    {
        [HttpGet]
        [Route("userapi/neighborhood/{nhoodId}/elections")]
        public IActionResult GetByNhood(uint nhoodId)
        {
            var api = Api.INSTANCE;

            using (var da = api.DAFactory.Get())
            {
                var nhood = da.Neighborhoods.Get(nhoodId);
                if (nhood.election_cycle_id == null) return ApiResponse.Json(HttpStatusCode.NotFound, new JSONElectionError("Election cycle not found"));

                var electionCycle = da.Elections.GetCycle((uint)nhood.election_cycle_id);
                
                var electionCandidates = new List<DbElectionCandidate>();
                if (electionCycle.current_state == Database.DA.Elections.DbElectionCycleState.election)
                    electionCandidates = da.Elections.GetCandidates(electionCycle.cycle_id, Database.DA.Elections.DbCandidateState.running);

                if (electionCycle.current_state == Database.DA.Elections.DbElectionCycleState.ended)
                    electionCandidates = da.Elections.GetCandidates(electionCycle.cycle_id, Database.DA.Elections.DbCandidateState.won);

                if (electionCycle == null) return ApiResponse.Json(HttpStatusCode.NotFound, new JSONElectionError("Election cycle not found"));

                List<JSONCandidates> candidatesJson = new List<JSONCandidates>();
                foreach (var candidate in electionCandidates)
                {
                    candidatesJson.Add(new JSONCandidates
                    {
                       candidate_avatar_id = candidate.candidate_avatar_id,
                       comment = candidate.comment,
                       state = candidate.state
                    });

                }
                var electionJson = new JSONElections();
                electionJson.candidates = candidatesJson;
                electionJson.current_state = electionCycle.current_state;
                electionJson.neighborhood_id = nhood.neighborhood_id;
                electionJson.start_date = electionCycle.start_date;
                electionJson.end_date = electionCycle.end_date;
                return ApiResponse.Json(HttpStatusCode.OK, electionJson);
            }
        }
    }
    public class JSONElectionError
    {
        public string error;
        public JSONElectionError(string errorString)
        {
            error = errorString;
        }
    }
    public class JSONElections
    {
        public DbElectionCycleState current_state { get; set; }
        public int neighborhood_id { get; set; }
        public uint start_date { get; set; }
        public uint end_date { get; set; }
        public List<JSONCandidates> candidates { get; set; }
    }
    public class JSONCandidates
    {
        public uint candidate_avatar_id { get; set; }
        public string comment { get; set; }
        public DbCandidateState state { get; set; }
    }
}
