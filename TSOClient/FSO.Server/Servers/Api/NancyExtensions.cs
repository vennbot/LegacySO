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
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using FSO.Server.Database.DA.Utils;
using FSO.Common.Utils;
using System.Xml;
using FSO.Server.Servers.Api.JsonWebToken;

namespace FSO.Server.Servers.Api
{
    public static class NancyExtensions
    {
        public static void DemandModerator(this NancyModule controller)
        {
            controller.RequiresAuthentication();
            var user = (JWTUserIdentity)controller.Context.CurrentUser;
            user.Claims.Contains("moderator");
        }

        public static void DemandAdmin(this NancyModule controller)
        {
            controller.RequiresAuthentication();
            var user = (JWTUserIdentity)controller.Context.CurrentUser;
            user.Claims.Contains("admin");
        }

        public static Response AsPagedList<T>(this IResponseFormatter formatter, PagedList<T> list)
        {
            return FormatterExtensions.AsJson<PagedList<T>>(formatter, list)
                        .WithHeader("X-Total-Count", list.Total.ToString())
                        .WithHeader("X-Offset", list.Offset.ToString());
        }

        public static Response AsXml(this IResponseFormatter formatter, IXMLEntity entity)
        {
            var doc = new XmlDocument();
            var firstChild = entity.Serialize(doc);
            doc.AppendChild(firstChild);

            return FormatterExtensions.AsText(formatter, doc.OuterXml).WithContentType("text/xml");
        }
    }
}
