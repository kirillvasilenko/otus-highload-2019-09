using Amursoft.DbMigrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace UsersService.Migrator.MySql
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            
            var migratorConfig = config.GetSection("Users").Get<MigratorConfig>();
            var dbMigrator = DbMigrator.MakeMySql(migratorConfig, CreateLogger());
            var result = dbMigrator.Migrate();

            return result.Successful
                ? 0
                : -1;
        }

        private static ILogger CreateLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            var loggerFactory = new SerilogLoggerFactory(logger);
            return loggerFactory.CreateLogger<Program>();
        }
    }
}