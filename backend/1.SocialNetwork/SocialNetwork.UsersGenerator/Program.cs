using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Serilog.Extensions.Logging;
using SocialNetwork.Model;
using SocialNetwork.Repo.MySql;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SocialNetwork.UsersGenerator
{
 

    class Program
    {
        private static int step;
        private static int count;
        private static int threadsCount;
        private static bool onlyIfDbEmpty;
        private static string connectionString;
        private static Pbkdf2PasswordHasher hasher;
        private static ILogger logger;
        private static int generated = 0;
        
        static async Task Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            step = 50;
            count = config.GetValue("Count", 1000);
            threadsCount = config.GetValue("ThreadsCount", 10);
            onlyIfDbEmpty = config.GetValue("OnlyIfDbIsEmpty", true);
            connectionString = config["ConnectionString"];

            logger = CreateLogger();
            hasher = new Pbkdf2PasswordHasher(new Pdkdf2PasswordHasherOptions());

            if (!await IsGenerationNeeded())
            {
                return;
            }
            
            var tasks = new List<Task>();
            for (int i = 0; i < threadsCount; i++)
            {
                tasks.Add(Task.Factory.StartNew(GenerateImpl));
            }

            Task.WaitAll(tasks.ToArray());

            logger.LogInformation($"Total generated users: {generated}");
            /*var admin = new User
            {
                Age = 20,
                City = "Муха",
                Email = "admin@adm.adm",
                GivenName = "Админ",
                FamilyName = "Административный",
                IsActive = true,
                Password = hasher.HashPassword("123"),
                Interests = "администрирование"
            };
            await repo.AddUser(admin);*/
        }

        private static async Task<bool> IsGenerationNeeded()
        {
            if (!onlyIfDbEmpty) return true;
            
            await using var connectionController = new DbConnectionControllerMySql(connectionString);
            var repo = new UsersRepoMySql(connectionController, new NullLogger<UsersRepoMySql>());
            await OpenConnection(connectionController);
            
            var totalCount = await repo.GetUsersCount(new GetUsersQuery());
            return totalCount == 0;
        } 

        private static async Task GenerateImpl()
        {
            await using var connectionController = new DbConnectionControllerMySql(connectionString);
            var repo = new UsersRepoMySql(connectionController, new NullLogger<UsersRepoMySql>());
            await OpenConnection(connectionController);

            for (var idxFrom = 0; idxFrom < count; idxFrom+=step)
            {
                var idxTo = idxFrom + step < count
                    ? idxFrom + step
                    : count;
                var testUsers = new Faker<User>(/*"ru"*/)
                    .RuleFor(u => u.Id, (f, u) => 0)
                    .RuleFor(u => u.GivenName, (f, u) => f.Name.FirstName())
                    .RuleFor(u => u.FamilyName, (f, u) => f.Name.LastName())
                    .RuleFor(u => u.Email, (f,u) => f.Internet.Email(u.GivenName, u.FamilyName, null, f.UniqueIndex.ToString()))
                    .RuleFor(u => u.Password, (f, u) => hasher.HashPassword("123"))
                    .RuleFor(u => u.Age, (f, u) => f.Random.Byte(5,80))
                    .RuleFor(u => u.City, f => f.Address.City())
                    .RuleFor(u => u.IsActive, f => true);
                
                var users = testUsers.Generate(idxTo - idxFrom);
                foreach (var user in users)
                {
                    await repo.AddUser(user);
                    Interlocked.Increment(ref generated);
                }

                logger.LogInformation($"{generated} have been generated.");
            }
        }

        private static async Task OpenConnection(IDbConnectionController controller)
        {
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    await controller.OpenConnectionAsync();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
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