using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Amursoft.AspNetCore.TestAuthentication;
using Hellang.Middleware.ProblemDetails;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using NSwag.Generation.Processors;
using UsersService.Model;
using UsersService.Repo.MySql;
using YadnexTank.PhantomAmmo.AspNetCore;

namespace UsersService.AspNet
{
// Extension method used to add the middleware to the HTTP request pipeline.

    public class Startup
    {
        private const string Version = "v1";
        
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = Config.GetSection("Logging").GetValue("ShowPII", false);
            
            services.AddControllers();
            ConfigureAuthentication(services);
            
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = Version;
                    document.Info.Title = $"{Program.AppName} API";	
                };
                config.OperationProcessors.Add(new OperationProcessor(ctx =>
                {
                    ctx.OperationDescription.Operation.OperationId = ctx.MethodInfo.Name;
                    return true;
                }));
            });
            
            services.AddReposMySql(Config.GetConnectionString("UsersDb"));
            
            services.AddPhantomAmmoCollector(Config.GetSection("PhantomAmmoCollector"));
            services.AddProblemDetails(opts =>
            {
                opts.Map<ItemNotFoundException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status400BadRequest));
                opts.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            if (Config.GetSection("Auth").GetValue("UseTestAuth", false))
            {
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuth.SchemeName;
                        options.DefaultChallengeScheme = TestAuth.SchemeName;
                    })
                    .AddTestAuth();
                return;
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Config["Auth:AuthorityUrl"];
                options.RequireHttpsMetadata = Config.GetSection("Auth").GetValue("RequireHttpsMetadata", true);

                options.Audience = "webapp";

                options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Subject;
                options.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;

                var tokenValidator = options.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
                tokenValidator.MapInboundClaims = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();
            app.UsePhantomAmmoCollector();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}