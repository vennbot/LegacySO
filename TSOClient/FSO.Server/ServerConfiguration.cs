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
using FSO.Server.Database;
using FSO.Server.Discord;
using FSO.Server.Servers.Api.JsonWebToken;
using FSO.Server.Servers.City;
using FSO.Server.Servers.Lot;
using FSO.Server.Servers.Tasks;
using FSO.Server.Servers.UserApi;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.IO;

namespace FSO.Server
{
    public class ServerConfiguration
    {
        public string GameLocation;
        public string SimNFS;
        public string UpdateBranch;

        public DatabaseConfiguration Database;
        public ServerConfigurationservices Services;
        public DiscordConfiguration Discord;

        /// <summary>
        /// Secret string used as a key for signing JWT tokens for the admin system
        /// </summary>
        public string Secret;

        /// <summary>
        /// Update ID this server is running on. All shards that we host will report needing this version, and this is reported with our host information.
        /// Loaded from updateID.txt if present.
        /// </summary>
        public int? UpdateID;
    }


    public class ServerConfigurationservices
    {
        public ApiServerConfiguration UserApi;
        public TaskServerConfiguration Tasks;
        public List<CityServerConfiguration> Cities;
        public List<LotServerConfiguration> Lots;
    }

    

    public class ServerConfigurationModule : NinjectModule
    {
        private ServerConfiguration GetConfiguration(IContext context)
        {
            //TODO: Allow config path to be overriden in a switch
            var configPath = "config.json";
            if (!File.Exists(configPath))
            {
                throw new Exception("Configuration file, config.json, missing");
            }

            var data = File.ReadAllText(configPath);

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ServerConfiguration>(data);
            }catch(Exception ex)
            {
                throw new Exception("Could not deserialize config.json", ex);
            }
        }

        private class DatabaseConfigurationProvider : IProvider<DatabaseConfiguration>
        {
            private ServerConfiguration Config;

            public DatabaseConfigurationProvider(ServerConfiguration config)
            {
                this.Config = config;    
            }


            public Type Type
            {
                get
                {
                    return typeof(DatabaseConfiguration);
                }
            }

            public object Create(IContext context)
            {
                return this.Config.Database;
            }
        }


        private class JWTConfigurationProvider : IProvider<JWTConfiguration>
        {
            private ServerConfiguration Config;

            public JWTConfigurationProvider(ServerConfiguration config)
            {
                this.Config = config;
            }


            public Type Type
            {
                get
                {
                    return typeof(JWTConfiguration);
                }
            }

            public object Create(IContext context)
            {
                return new JWTConfiguration() {
                    Key = System.Text.UTF8Encoding.UTF8.GetBytes(Config.Secret)
                };
            }
        }

        public override void Load()
        {
            this.Bind<ServerConfiguration>().ToMethod(new Func<Ninject.Activation.IContext, ServerConfiguration>(GetConfiguration)).InSingletonScope();
            this.Bind<DatabaseConfiguration>().ToProvider<DatabaseConfigurationProvider>().InSingletonScope();
            this.Bind<JWTConfiguration>().ToProvider<JWTConfigurationProvider>().InSingletonScope();
        }
    }
}
