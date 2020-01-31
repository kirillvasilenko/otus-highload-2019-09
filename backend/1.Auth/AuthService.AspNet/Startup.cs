using System;
using System.Collections.Generic;
using System.IO;
using AuthService.Users;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialNetwork.AspNet.Utils;
using YadnexTank.PhantomAmmo.AspNetCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SocialNetwork.AspNet
{
    public class Startup
    {
        private const string Version = "v1";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddAll(Configuration.GetConnectionString("UsersDb"));

            services.AddIdentityServer()
                .LoadSigningCredentialFrom(Configuration["SigningCertificate:Path"],
                    Configuration["SigningCertificate:Password"])
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddUsers();
            
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

            services.AddPhantomAmmoCollector(Configuration.GetSection("PhantomAmmoCollector"));
            services.AddProblemDetails(opts =>
            {
                opts.Map<UserRegistrationException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status400BadRequest));
                opts.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();
            app.UsePhantomAmmoCollector();
            
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                    });
                })
                .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint($"../swagger/{Version}/swagger.json",
                            $"{Program.AppName} API");
                        c.RoutePrefix = "swagger";
                        
                    }
                    
                );
            
            app.UseRouting();

            app.UseIdentityServer();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}