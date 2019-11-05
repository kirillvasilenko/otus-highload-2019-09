using System;
using System.IO;
using System.Linq;
using DbUp;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.ScriptProviders;
using MySql.Data.MySqlClient;

namespace SocialNetwork.Repo.MySql.Migrator
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                return ReturnError(
                    "Invalid args. You have to specify connection string and scripts path");
            }
 
            var connectionString = args[0];
            var scriptsPath = args.ElementAtOrDefault(1) ?? string.Empty;
            
            Console.WriteLine("Start executing migration scripts...");
            var migrationScriptsPath = Path.Combine(scriptsPath, "Migrations");
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