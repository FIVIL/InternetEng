using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InternetProject.Models;
using InternetProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace InternetProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly Context context;
        private readonly SessionProvider session;
        private readonly IHostingEnvironment environment;
        public CarController(Context _context, SessionProvider _session, IHostingEnvironment _environment)
        {
            context = _context;
            session = _session;
            environment = _environment;
        }
        [HttpGet, Route("Brands")]
        public IEnumerable<CarBrand> CarBrands()
        {
            return context.Brands;
        }
        [HttpGet, Route("Colors")]
        public IEnumerable<CarColor> CarColors()
        {
            return context.Colors;
        }
        [HttpGet, Route("Today")]
        public async Task<IActionResult> Today()
        {
            var data = await context.Ads
                .Include(x => x.Color)
                .Include(x => x.Images)
                .Include(x => x.Car)
                    .ThenInclude(y => y.Brand)
                .Where(x => x.AdTime > DateTime.Now.AddDays(-1) && x.Verified && !x.Expired)
                .ToListAsync();
            foreach (var item in data)
            {
                if (item.Images != null && item.Images.Count != 0)
                {
                    foreach (var item2 in item.Images)
                    {
                        item2.Value = await Download(item2.Name);
                    }
                }
            }
            return Ok(data);
        }
        [HttpGet, Route("Detailes/{id}")]
        public async Task<IActionResult> Detailes(Guid id)
        {
            var data = await context.Ads
                .Include(x => x.Color)
                .Include(x => x.Images)
                .Include(x => x.Owner)
                    .ThenInclude(y => y.City)
                .Include(x => x.Car)
                    .ThenInclude(y => y.Brand)
                .FirstOrDefaultAsync(x => x.ID == id);
            if (data == null) return NotFound(new Ad() { ID = Guid.Empty });
            if (data.Images != null && data.Images.Count != 0)
            {
                foreach (var item2 in data.Images)
                {
                    item2.Value = await Download(item2.Name);
                }
            }
            return Ok(data);
        }
        [HttpPost, Route("Search")]
        public async Task<IActionResult> SearchAsync([FromBody]SearchViewModel VMS)
        {
            var data = await context.Ads
                .Include(x => x.Color)
                .Include(x => x.Images)
                .Include(x => x.Car)
                    .ThenInclude(y => y.Brand)
                .Where(x => x.Verified && !x.Expired)
                .Skip(VMS.Skip * 18)
                .Take(18)
                .ToListAsync();
            if (VMS.Brand != null)
            {
                data = data.Where(x => x.Car.BrandID == VMS.Brand).ToList();
            }
            if (!string.IsNullOrEmpty(VMS.CarName))
            {
                data = data.Where(x => x.Car.CarName.ToLower().Trim() == VMS.CarName.ToLower().Trim()).ToList();
            }
            if (VMS.Fuel != null)
            {
                data = data.Where(x => x.Car.Fuel == VMS.Fuel).ToList();
            }
            if (VMS.Gearbox != null)
            {
                data = data.Where(x => x.Car.Gearbox == VMS.Gearbox).ToList();
            }
            if (VMS.CarClass != null)
            {
                data = data.Where(x => x.Car.CarClass == VMS.CarClass).ToList();
            }
            if (VMS.MYearStart != null)
            {
                data = data.Where(x => x.ManufacturingDate.Year >= VMS.MYearStart).ToList();
            }
            if (VMS.MYearEnd != null)
            {
                data = data.Where(x => x.ManufacturingDate.Year <= VMS.MYearEnd).ToList();
            }
            if (VMS.KMS != null)
            {
                data = data.Where(x => x.KM >= VMS.KMS).ToList();
            }
            if (VMS.KME != null)
            {
                data = data.Where(x => x.KM <= VMS.KME).ToList();
            }
            if (VMS.PriceS != null)
            {
                data = data.Where(x => x.Price >= VMS.PriceS).ToList();
            }
            if (VMS.PriceE != null)
            {
                data = data.Where(x => x.Price <= VMS.PriceE).ToList();
            }
            if (VMS.FirstHanded != null)
            {
                data = data.Where(x => x.FirstHanded == VMS.FirstHanded).ToList();
            }
            if (VMS.Planned != null)
            {
                data = data.Where(x => x.PlanedPayment == VMS.Planned).ToList();
            }
            if (VMS.HavePic != null)
            {
                if ((bool)VMS.HavePic) data = data.Where(x => x.Images.Any()).ToList();
            }
            foreach (var item in data)
            {
                if (item.Images != null && item.Images.Count != 0)
                {
                    foreach (var item2 in item.Images)
                    {
                        item2.Value = await Download(item2.Name);
                    }
                }
            }
            return Ok(data);
        }
        [HttpPost, Route("Upload")]
        public async Task<string> Upload(/*[FromBody]Image img*/)
        {
            //var file = HttpContext.Request.Headers["file"].ToString();     
            //    var content = img.Value;
            //    var add = Guid.NewGuid().ToString();
            //    var path = Path.Combine(environment.ContentRootPath, "Img", add);
            //    await System.IO.File.WriteAllTextAsync(path, content);
            //    var id = Guid.NewGuid();
            //    context.Images.Add(new Image
            //    {
            //        ID = id,
            //        Name = add
            //    });
            //    await context.SaveChangesAsync();
            //    return id.ToString();
            return "";
        }
        //[HttpGet, Route("Download/{id}")]
        private async Task<string> Download(string id)
        {
            var path = Path.Combine(environment.ContentRootPath, "Img", id);
            return await System.IO.File.ReadAllTextAsync(path);
        }
        [HttpGet, Route("Verify")]
        public async Task<IActionResult> GetForVerify()
        {
            if (session.AdminToken == Guid.Empty) return Forbid();
            var tok = HttpContext.Request.Headers["AdminToken"];
            if (tok.ToString() != session.AdminToken.ToString()) return BadRequest();
            var data = await context.Ads
            .Include(x => x.Color)
            .Include(x => x.Images)
            .Include(x => x.Car)
                .ThenInclude(y => y.Brand)
            .Where(x => !x.Expired)
            .ToListAsync();
            foreach (var item in data)
            {
                if (item.Images != null && item.Images.Count != 0)
                {
                    foreach (var item2 in item.Images)
                    {
                        item2.Value = await Download(item2.Name);
                    }
                }
            }
            return Ok(data);
        }

        [HttpPost, Route("VerifyResult")]
        public async Task<IActionResult> GetForVerifyResult([FromBody]IList<Ad> ads)
        {
            if (session.AdminToken == Guid.Empty) return Forbid();
            var tok = HttpContext.Request.Headers["AdminToken"];
            if (tok.ToString() != session.AdminToken.ToString()) return BadRequest();
            foreach (var item in ads)
            {
                //if (item.Verified)
                //{
                context.Entry(item).State = EntityState.Modified;
                context.Ads.Update(item);
                //}
            }
            await context.SaveChangesAsync();
            var data = await context.Ads
                .Include(x => x.Color)
                .Include(x => x.Images)
                .Include(x => x.Car)
                    .ThenInclude(y => y.Brand)
                .Where(x => !x.Expired)
                .ToListAsync();
            foreach (var item in data)
            {
                if (item.Images != null && item.Images.Count != 0)
                {
                    foreach (var item2 in item.Images)
                    {
                        item2.Value = await Download(item2.Name);
                    }
                }
            }
            return Ok(data);
        }
        [HttpPost, Route("newcar")]
        public async Task<IActionResult> NewCar([FromBody] Ad ad)
        {
            var recaptch = ad.Recaptcha;
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LeI7mIUAAAAAIktUrVfGXBzvQXjJN6tYx1fTrLj&response={recaptch}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(new
                {
                    Name = "WR",
                    res = false
                });
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return BadRequest(new
                {
                    Name = "WR",
                    res = false
                });
            }
            ad.ID = Guid.NewGuid();
            ad.Verified = false;
            ad.Expired = false;
            ad.AdTime = DateTime.Now;
            var owner = await context.Accounts.FindAsync(ad.OwnerID);
            if (owner == null) return BadRequest(new
            {
                Name = "No Owner",
                res = false
            });
            context.Entry(owner).State = EntityState.Modified;
            var car = await context.Cars.FirstOrDefaultAsync(
                x => x.BrandID == ad.Car.BrandID
                && x.CarClass == ad.Car.CarClass
                && x.Fuel == ad.Car.Fuel
                && x.Gearbox == ad.Car.Gearbox
                && x.CarName.Trim().ToLower() == ad.Car.CarName.Trim().ToLower());
            if (car == null)
            {
                car = new CarType()
                {
                    ID = Guid.NewGuid(),
                    BrandID = ad.Car.BrandID,
                    CarClass = ad.Car.CarClass,
                    CarName = ad.Car.CarName,
                    Fuel = ad.Car.Fuel,
                    Gearbox = ad.Car.Gearbox
                };
                context.Cars.Add(car);
                context.Entry(car).State = EntityState.Added;
            }
            else
            {
                context.Entry(car).State = EntityState.Modified;
            }
            ad.Car = null;
            ad.CarID = car.ID;
            if (ad.Images != null)
            {
                if (ad.Images.Count > 0)
                {
                    foreach (var item in ad.Images)
                    {
                        var img = await context.Images.FindAsync(Guid.Parse(item.Name));
                        img.AdID = ad.ID;
                        context.Entry(img).State = EntityState.Modified;
                    }
                }
            }
            ad.Images = null;
            context.Entry(ad).State = EntityState.Added;
            context.Ads.Add(ad);
            await context.SaveChangesAsync();
            return Ok(new
            {
                Name = ad.ID.ToString(),
                res = true
            });
        }
    }
}