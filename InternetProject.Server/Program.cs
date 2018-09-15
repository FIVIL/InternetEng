using InternetProject.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace InternetProject.Server
{
    public class Program
    {
        private static bool IsSeeded = false;
        public static void Main(string[] args)
        {
            if (!IsSeeded)
            {
                IsSeeded = true;
                var option = new DbContextOptionsBuilder<Context>();
                var Configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                var options = option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options;
                using (var context = new Context(options))
                {
                    Seed(context);
                }
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
        private static string Sha256(string input)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(input);
            string res = string.Empty;
            using (var sha256 = SHA256.Create())
            {
                res = Convert.ToBase64String(sha256.ComputeHash(data));
            }
            return res;
        }
        private static void Seed(Context context)
        {
            var ads = context.Ads.ToList();
            for (int i = 0; i < ads.Count; i++)
            {
                if(i%2==0)ads[i].Price = 6000000;
                context.Entry(ads[i]).State = EntityState.Modified;
                context.Ads.Update(ads[i]);
            }
            context.SaveChanges();
            //var ads = context.Ads.ToList();
            //for (int i = 10; i < ads.Count; i++)
            //{
            //    ads[i].AdTime = DateTime.Now.AddDays(-3);
            //    ads[i].Expired = false;
            //    context.Entry(ads[i]).State = EntityState.Modified;
            //}
            //context.SaveChanges();
            //var carid = context.Cars.First().ID;
            //var colorid= context.Colors.First().ID;
            //var userid = context.Accounts.First().ID;
            //for (int i = 0; i < 100; i++)
            //{
            //    var add = new Ad()
            //    {
            //        ID = Guid.NewGuid(),
            //        Address = "21St,West Avenue",
            //        AdTime = DateTime.Now,
            //        CarID = carid,
            //        ColorID = colorid,
            //        Expired = false,
            //        FirstHanded = true,
            //        Insurance = true,
            //        InsuranceExpirationDate = DateTime.Now.AddYears(1),
            //        KM = 0.1,
            //        ManufacturingDate = DateTime.Now.AddMonths(-1),
            //        OwnerID = userid,
            //        PlanedPayment = false,
            //        Price = 55000000,
            //        TechnicalInspection = true,
            //        Verified = true
            //    };
            //    context.Ads.Add(add);
            //    context.Entry(add).State = EntityState.Added;
            //}
            //context.SaveChanges();
            if (context.Accounts.Any()) return;
            Guid ID;
            var city = new CityName()
            {
                ID = Guid.NewGuid(),
                Name = "Tehran"
            };
            context.Cities.Add(city);
            context.Entry(city).State = EntityState.Added;
            var User = new Account()
            {
                ID = Guid.NewGuid(),
                CityID = city.ID,
                IsAdmin = true,
                UserName = "FIVIL",
                Name = "Hamed",
                Password = Sha256("rafcvqxb2"),
                Email = "a@b.c",
                LastName = "Mohammadi",
                PhoneNumber = "09123425654"
            };
            context.Accounts.Add(User);
            context.Entry(User).State = EntityState.Added;
            city = new CityName()
            {
                ID = Guid.NewGuid(),
                Name = "Mashhad"
            };
            context.Cities.Add(city);
            context.Entry(city).State = EntityState.Added;
            city = new CityName()
            {
                ID = Guid.NewGuid(),
                Name = "Shiraz"
            };
            context.Cities.Add(city);
            context.Entry(city).State = EntityState.Added;
            city = new CityName()
            {
                ID = Guid.NewGuid(),
                Name = "Karaj"
            };
            context.Cities.Add(city);
            context.Entry(city).State = EntityState.Added;
            city = new CityName()
            {
                ID = Guid.NewGuid(),
                Name = "Nyc"
            };
            context.Cities.Add(city);
            context.Entry(city).State = EntityState.Added;
            User = new Account()
            {
                ID = Guid.NewGuid(),
                CityID = city.ID,
                IsAdmin = true,
                UserName = "Billy",
                Name = "William",
                Password = Sha256("12345"),
                Email = "d@e.f",
                LastName = "Deafou",
                PhoneNumber = "09120978675"
            };
            context.Accounts.Add(User);
            context.Entry(User).State = EntityState.Added;
            var car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "ALFA ROMEO",
                ImgPath = "AR"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Audi",
                ImgPath = "AUDI"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "BMW",
                ImgPath = "BMW"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Brilliance",
                ImgPath = "brilliance"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Cherry",
                ImgPath = "Cherry"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Citroen",
                ImgPath = "Citroen"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Ferari",
                ImgPath = "ferari"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Ford",
                ImgPath = "Ford"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Geely",
                ImgPath = "Geely"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Hyundai",
                ImgPath = "Hyundai"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Iran Khodro",
                ImgPath = "IKCO"
            };
            ID = car.ID;
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Jac",
                ImgPath = "jac"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Jaguar",
                ImgPath = "jaguar"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Jeep",
                ImgPath = "jeep"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "KIA motors",
                ImgPath = "KIA"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Lexus",
                ImgPath = "lexus"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "LIFAN",
                ImgPath = "lifan"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Maserati",
                ImgPath = "Maserati"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Mazda",
                ImgPath = "mazda"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Mercedes Benz",
                ImgPath = "Mbenz"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "MG",
                ImgPath = "MG"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Mitsubishi Motors",
                ImgPath = "Mitsubishi"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Nissan",
                ImgPath = "Nissan"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Opel",
                ImgPath = "Opel"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Pars Khodro",
                ImgPath = "ParsKhodro"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Peugeot",
                ImgPath = "Peugeot"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Porcshe",
                ImgPath = "Porcshe"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Renault",
                ImgPath = "Renault"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Rolls Royce",
                ImgPath = "RollsRoyce"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Saipa",
                ImgPath = "saipa"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Suzuki",
                ImgPath = "suzuki"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Tesla Motors",
                ImgPath = "Tesla"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "TOYOTA",
                ImgPath = "toyota"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Volks Wagen",
                ImgPath = "Volkswagen"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Volvo",
                ImgPath = "Volvo"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            car = new CarBrand()
            {
                ID = Guid.NewGuid(),
                Name = "Zamyad",
                ImgPath = "zamyad"
            };
            context.Brands.Add(car);
            context.Entry(car).State = EntityState.Added;
            var color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "White",
                Code = "#FFFFFF"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Silver",
                Code = "#C0C0C0"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Gray",
                Code = "#808080"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Black",
                Code = "#000000"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Red",
                Code = "#FF0000"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Blue",
                Code = "#0000FF"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Green",
                Code = "#008000"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            color = new CarColor()
            {
                ID = Guid.NewGuid(),
                Color = "Metallic gold",
                Code = "#D4AF37"
            };
            context.Colors.Add(color);
            context.Entry(color).State = EntityState.Added;
            var Car = new CarType()
            {
                ID = Guid.NewGuid(),
                BrandID = ID,
                CarClass = CarClassType.Sedans,
                CarName = "Samand LX",
                Fuel = FuelType.Petrol,
                Gearbox = GearboxType.Manual
            };
            context.Cars.Add(Car);
            context.Entry(Car).State = EntityState.Added;
            Car = new CarType()
            {
                ID = Guid.NewGuid(),
                BrandID = ID,
                CarClass = CarClassType.Sedans,
                CarName = "Dena",
                Fuel = FuelType.Petrol,
                Gearbox = GearboxType.Automatic
            };
            context.Cars.Add(Car);
            context.Entry(Car).State = EntityState.Added;
            var ad = new Ad()
            {
                ID = Guid.NewGuid(),
                Address = "21St,West Avenue",
                AdTime = DateTime.Now,
                CarID = Car.ID,
                ColorID = color.ID,
                Expired = false,
                FirstHanded = true,
                Insurance = true,
                InsuranceExpirationDate = DateTime.Now.AddYears(1),
                KM = 0.1,
                ManufacturingDate = new DateTime(DateTime.Now.Year, 1, 10),
                OwnerID = User.ID,
                PlanedPayment = false,
                Price = 55000000,
                TechnicalInspection = true,
                Verified = true
            };
            context.Ads.Add(ad);
            context.Entry(ad).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}
