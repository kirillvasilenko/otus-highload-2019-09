using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
using Microsoft.OpenApi.Models;
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
            services.AddUsersService();
            services.AddReposMySql(Config.GetConnectionString("UsersDb"));
            
            services.AddControllers();

            ConfigureAuthentication(services);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo
                {
                    Title = Program.AppName,
                    Version = Version
                });

                //Set the comments path for the swagger json and ui.
                foreach (var docFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    c.IncludeXmlComments(docFile, true);
                }
            });

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
            
            app.UseSwagger()
                .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint($"/swagger/{Version}/swagger.json",
                            $"{Program.AppName} API");
                    }
                );

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}