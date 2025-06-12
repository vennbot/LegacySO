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
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace FSO.Server.Api.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IAPILifetime RunAsync(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var lifetime = new APIControl((IApplicationLifetime)host.Services.GetService(typeof(IApplicationLifetime)));
            host.Start();
            return lifetime;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(args[0])
                .ConfigureLogging(x =>
                {
                    x.SetMinimumLevel(LogLevel.None);
                })
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = 500000000;
                })
                .SuppressStatusMessages(true)
                .UseStartup<Startup>();
    }

    public class APIControl : IAPILifetime
    {
        private IApplicationLifetime Lifetime;
        
        public APIControl(IApplicationLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public void Stop()
        {
            Lifetime.StopApplication();
        }
    }
}
