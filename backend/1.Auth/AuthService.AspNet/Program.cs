using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SocialNetwork.AspNet
{
    public class Program
    {
        public const string AppName = "AuthService";
        
        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{currentEnv}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", "AuthService")
                .Enrich.WithProperty("Environment", currentEnv)
                .CreateLogger();

            IHostBuilder host = Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseConfiguration(configuration)
                        .UseSerilog()
                        .UseStartup<Startup>();
                });

            try
            {
                Log.Logger.Information("Starting web host");
                host.Build().Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Web Host terminated unexpectedly");
            }
            finally
            {
                Log.Logger.Information("Finally web host");
                Log.CloseAndFlush();
            }
        }
    }
}