using System;
using System.IO;
using System.Linq;
using DbUp;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.ScriptProviders;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace UsersService.Repo.MySql.Migrator
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            
            var connectionString = configuration.GetConnectionString("UsersDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return ReturnError(
                    "Invalid args. You have to specify connection string and scripts path");
            }
            
            var scriptsPath = AppContext.BaseDirectory;
            var migrationScriptsPath = Path.Combine(scriptsPath, "Migrations");
            
            Console.WriteLine("Start executing migration scripts...");
            
            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(migrationScriptsPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole()
                    .Build();
 
            var result = upgrader.PerformUpgrade();
 
            if (!result.Successful)
            {
                return ReturnError(result.Error.ToString());
            }
 
            ShowSuccess();
            return 0;
        }
 
        private static void ShowSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }
 
        private static int ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            return -1;
        }
    }
}