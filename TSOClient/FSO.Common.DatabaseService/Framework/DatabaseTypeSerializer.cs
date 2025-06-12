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
using FSO.Common.Serialization.TypeSerializers;
using System;
using System.Reflection;
using FSO.Common.DatabaseService.Model;

namespace FSO.Common.DatabaseService.Framework
{
    public class DatabaseTypeSerializer : cTSOValueDecorated
    {
        protected override void ScanAssembly(Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    System.Attribute[] attributes = System.Attribute.GetCustomAttributes(type);

                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute is DatabaseRequest)
                        {
                            var request = (DatabaseRequest)attribute;
                            uint requestId = DBRequestTypeUtils.GetRequestID(request.Type);

                            ClsIdToType.Add(requestId, type);
                            if (!TypeToClsId.ContainsKey(type))
                            {
                                TypeToClsId.Add(type, requestId);
                            }
                        }
                        else if (attribute is DatabaseResponse)
                        {
                            var response = (DatabaseResponse)attribute;
                            uint responseId = DBResponseTypeUtils.GetResponseID(response.Type);

                            ClsIdToType.Add(responseId, type);
                            if (!TypeToClsId.ContainsKey(type))
                            {
                                TypeToClsId.Add(type, responseId);
                            }

                        }
                    }
                }
            } catch (Exception)
            {

            }
        }
    }
}
