using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationSamples
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("hasReportAccess", 
                    policy => policy
                        .RequireClaim("accesses", "report")
                        .RequireRole("user"));

                opt.AddPolicy("accessibleOnlyDuringOfficeHours", 
                    policy => policy.AddRequirements(new OfficeHoursRequirement(8, 23))
                        .RequireClaim("accesses", "report")
                        .RequireRole("user"));
            });

            services.AddSingleton<IAuthorizationHandler, OfficeHoursRequirementHandler>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var tokenValidationParameters = new TokenValidationParameters
            {
                // Disable all token integrity validations
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidateActor = false,
                RequireSignedTokens = false,
                NameClaimType = JwtRegisteredClaimNames.Sub,
                RoleClaimType = "roles"
            };

            // Remove all automatic mapping for inbound claims
            // Otherwise "sub" becomes "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc();
        }
    }
}
