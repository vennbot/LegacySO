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
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api
{
    public class ApiServerConfiguration
    {
        /// <summary>
        /// If true, the API server will attempt to bind
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Hostname bindings
        /// </summary>
        public List<string> Bindings { get; set; }

        /// <summary>
        /// Indicates which routes to register on the api
        /// </summary>
        public List<ApiServerControllers> Controllers { get; set; }
        
        /// <summary>
        /// How long an auth ticket is valid for
        /// </summary>
        public int AuthTicketDuration = 300;

        /// <summary>
        /// If non-null, the user must provide this key to register an account.
        /// </summary>
        public string Regkey { get; set; }

        /// <summary>
        /// If true, only authentication from moderators and admins will be accepted
        /// </summary>
        public bool Maintainance { get; set; }
        public string UpdateUrl { get; set; }
        public string CDNUrl { get; set; }

        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpUser { get; set; }
        public bool ForceEmailConfirmation { get; set; }
        public bool UseProxy { get; set; } = true;
    }

    public enum ApiServerControllers
    {
        Auth,
        CitySelector
    }
}
