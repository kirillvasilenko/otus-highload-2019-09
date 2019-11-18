using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UsersService.Repo.MySql;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace UsersService.AspNet
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
            services.AddControllers(options =>
                options.Filters.Add(typeof(CustomExceptionFilter)));
            
            services.AddUsersService();
            services.AddReposMySql(Configuration.GetConnectionString("MySql"));
            
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint($"/swagger/{Version}/swagger.json",
                            $"{Program.AppName} API");
                    }
                );
            
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}