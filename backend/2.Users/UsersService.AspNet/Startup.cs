using System;
using System.IO;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
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
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUsersService();
            services.AddReposMySql(Configuration.GetConnectionString("MySql"));
            
            /*services.AddAuthentication(options =>
            {
		
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "authority1";
                o.Audience = "audience1";
                o.RequireHttpsMetadata = false;
            });*/
            
            services.AddControllers( opts => opts.Filters.Add(new AllowAnonymousFilter()));
            
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
                opts.Map<ItemNotFoundException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status400BadRequest));
                opts.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
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

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}