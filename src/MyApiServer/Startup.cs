using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
            .AddAuthentication(GetAuthenticationOptions)
            .AddJwtBearer(GetJwtBearerOptions);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
            .UseAuthentication()
            .UseMvc();
        }

        private static void GetAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private static void GetJwtBearerOptions(JwtBearerOptions options)
        {
            options.Authority = Environment.GetEnvironmentVariable("IDENTITY_SERVER_AUTHORITY");
            options.Audience = Environment.GetEnvironmentVariable("IDENTITY_SERVER_AUDIENCE");
            options.RequireHttpsMetadata = bool.Parse(Environment.GetEnvironmentVariable("IDENTITY_SERVER_REQUIREHTTPSMETADATA"));
        }
    }
}