using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YandexTank.PhantomAmmo;

namespace SocialNetwork.LoadTests.Users
{
    class Program
    {
        private const string MysqlDb =
            "server=localhost;port=3306;database=socialnetwork;user=socialnetwork_app;password=socialnetwork_app";

        private const string FileName = "ammo.txt";
        
        private const int RequestsCount = 100;
        
        static void Main(string[] args)
        {
            var requestMaker = new RequestsMaker();

            var generator = new PhantomAmmoGeneratorBuilder()
                .AddSources(requestMaker.MakeGets())
                .Build();
            
            using (var file = File.CreateText(GetFilePath()))
            {
                for (int i = 0; i < RequestsCount; i++)
                {
                    file.Write(generator.GetNext());
                }    
            }
            
            Console.WriteLine($"Get:{requestMaker.GetCount}");

        }

        private static string GetFilePath()
        {
            var fullPath = Path.Combine("../../..", "tank", FileName);
            return Path.GetFullPath(fullPath);
        }
    }
}