
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
using FSO.Server.Framework.Gluon;
using FSO.Server.Framework.Voltron;
using NLog;
using System;
using System.Collections.Generic;

namespace FSO.Server.Framework.Aries
{
    public class AriesPacketRouter : IAriesPacketRouter
    {
        private static Logger LOG = LogManager.GetCurrentClassLogger();
        private Dictionary<Type, List<AriesHandler>> Handlers = new Dictionary<Type, List<AriesHandler>>();

        public void On<T>(AriesHandler handler)
        {
            this.On(typeof(T), handler);
        }

        public void Handle(IAriesSession session, object message)
        {
            var type = message.GetType();
            if (Handlers.ContainsKey(type))
            {
                foreach(var handler in Handlers[type])
                {
                    handler(session, message);
                }
            }
        }

        public void On(Type type, AriesHandler handler)
        {
            if (!Handlers.ContainsKey(type))
            {
                Handlers[type] = new List<AriesHandler>();
            }
            Handlers[type].Add(handler);
        }

        public void AddHandlers(object obj)
        {
            var methods = obj.GetType().GetMethods();

            foreach (var method in methods)
            {
                var args = method.GetParameters();

                if (method.Name.StartsWith("Handle") &&
                    args.Length == 2 &&
                    typeof(IVoltronSession).IsAssignableFrom(args[0].ParameterType))
                {
                    this.On(args[1].ParameterType, new AriesHandler(delegate (IAriesSession session, object msg)
                    {
                        if (session is IVoltronSession)
                        {
                            try {
                                method.Invoke(obj, new object[] { session, msg });
                            }catch(Exception ex){
                                LOG.Error(ex);
                            }
                        }
                    }));
                }
                else if (method.Name.StartsWith("Handle") &&
                 args.Length == 2 &&
                 typeof(IGluonSession).IsAssignableFrom(args[0].ParameterType))
                {
                    this.On(args[1].ParameterType, new AriesHandler(delegate (IAriesSession session, object msg)
                    {
                        if (session is IGluonSession)
                        {
                            try
                            {
                                method.Invoke(obj, new object[] { session, msg });
                            }
                            catch (Exception ex)
                            {
                                LOG.Error(ex);
                            }
                        }
                    }));
                }
                else if (method.Name.StartsWith("Handle") &&
                    args.Length == 2 &&
                    typeof(IAriesSession).IsAssignableFrom(args[0].ParameterType))
                {
                    this.On(args[1].ParameterType, new AriesHandler(delegate (IAriesSession session, object msg)
                    {
                        try {
                            method.Invoke(obj, new object[] { session, msg });
                        }catch(Exception ex){
                            LOG.Error(ex);
                        }
                    }));
                }

                
            }
        }
    }
}
