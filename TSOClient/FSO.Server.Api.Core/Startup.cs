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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSO.Server.Api.Core
{
    public class Startup : IAPILifetime
    {
        public IApplicationLifetime AppLifetime;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
               options.AddDefaultPolicy(
                   builder =>
                   {

                       builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("content-disposition");
                   });

                options.AddPolicy("AdminAppPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://legacyso.org", "http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("content-disposition");
                    });
            }).AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors();
            //app.UseHttpsRedirection();
            app.UseMvc();
            AppLifetime = appLifetime;
        }

        public void Stop()
        {
            AppLifetime.StopApplication();
        }
    }
}
