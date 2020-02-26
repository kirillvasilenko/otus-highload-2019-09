using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using Bogus;
using Bogus.DataSets;
using YandexTank.PhantomAmmo;

namespace SocialNetwork.LoadTests.Users
{
    public class RequestsMaker
    {

        private Random r = new Random(); 
        public int GetCount { get; private set; }

        private Faker faker = new Faker(/*"ru"*/);
        public Func<PhantomAmmoInfo>[] MakeGets()
        {
            return new Func<PhantomAmmoInfo>[]
            {
                () =>
                {
                    GetCount++;
                    var args = new Dictionary<string,string>();
                    
                    var gender = faker.PickRandom<Name.Gender>();
                    var firstName = faker.Name.FirstName(gender);
                    var lastName = faker.Name.LastName(gender);
                    var city = faker.Address.City();
                    
                    var minage = r.Next(10, 80);
                    var maxage = r.Next(minage, 81);
                    var (skip, take) = GenerateSkipAndTake();
                    
                    var condition = r.Next(0, 4);
                    if (condition == 0)
                    {
                        args["name"] = $"{lastName} {firstName}";
                    }else if (condition == 1)
                    {
                        args["name"] = $"{lastName}";
                    }
                    else if (condition == 2)
                    {
                        args["name"] = $"{firstName}";
                    }

                    condition = r.Next(0, 5);
                    if (condition == 0)
                    {
                        args["city"] = $"{city}";
                    }

                    condition = r.Next(0, 20);
                    if (condition == 0)
                    {
                        args["minage"] = $"{minage}";
                    }
                    else if (condition == 1)
                    {
                        args["maxage"] = $"{maxage}";
                    }
                    else if (condition == 2)
                    {
                        args["minage"] = $"{minage}";
                        args["maxage"] = $"{maxage}";
                    }

                    args["take"] = $"{take}";

                    var url = "/api/users";
                    return PhantomAmmoInfo.MakeGet(url, args);
                }
            };
        }
        
        private (int skip, int take) GenerateSkipAndTake()
        {
            // просто 100, можно получить более осмысленные значения, надо только посчитать количество
            // детей в каждом узле
            var skip = r.Next(0, 100); 
            var take = r.Next(1, 100);
            return (skip,take);
        }
    }
}