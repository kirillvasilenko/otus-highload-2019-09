using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Amursoft.AspNetCore.TestAuthentication;
using SocialNetwork.App;
using Hellang.Middleware.ProblemDetails;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors;
using SocialNetwork.Model;
using SocialNetwork.Repo.MySql;
using YadnexTank.PhantomAmmo.AspNetCore;

namespace SocialNetwork.AspNet
{
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
            services.AddRouting(option =>
            {
                option.LowercaseUrls = true;
            });
            
            ConfigureAuthentication(services);
            
            ConfigureSwagger(services);
            
            services.AddSocialNetworkApp(Config.GetSection("Auth"));
            services.AddSocialNetworkRepoMySql(Config.GetConnectionString("SocialNetworkDb"));

            services.AddPhantomAmmoCollector(Config.GetSection("PhantomAmmoCollector"));
            services.AddProblemDetails(opts =>
            {
                opts.Map<ItemNotFoundException>(ex => new StatusCodeProblemDetails(StatusCodes.Status404NotFound)
                    {Detail = ex.Message});
                opts.Map<AuthenticationException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest)
                    {Detail = ex.Message});
                opts.Map<UserRegistrationException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status400BadRequest));
                opts.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();
            app.UsePhantomAmmoCollector();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app.UseRouting();
            
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Config[$"Auth:{nameof(TokenMakerOptions.Issuer)}"],
                        
                    ValidAudience = Config[$"Auth:{nameof(TokenMakerOptions.Audience)}"],
                        
                    ClockSkew = TimeSpan.FromSeconds(0),
 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            Config[$"Auth:{nameof(TokenMakerOptions.Secret)}"])),
                                
                    NameClaimType = JwtClaimTypes.Subject,
                    RoleClaimType = JwtClaimTypes.Role,
                };
                var tokenValidator = options.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
                tokenValidator.MapInboundClaims = false;
            });
        }
    }
}