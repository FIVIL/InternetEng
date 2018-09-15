using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternetProject.Models;
using Microsoft.EntityFrameworkCore;
using InternetProject.ViewModels;
using System.Security.Cryptography;
namespace InternetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Context context;
        private readonly SessionProvider session;
        public AccountController(Context _context, SessionProvider _session)
        {
            context = _context;
            session = _session;
        }
        [HttpPost, Route("signup")]
        public async Task<IActionResult> Signup([FromBody]Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ((await context.Accounts.FirstOrDefaultAsync(x => x.UserName == account.UserName)) != null) return BadRequest();
            account.ID = Guid.NewGuid();
            account.Password = Sha256(account.Password);
            account.IsAdmin = false;
            context.Accounts.Add(account);
            context.Entry(account).State = EntityState.Added;
            await context.SaveChangesAsync();
            await session.Set(account.ID);
            return Ok(new
            {
                Name = account.ID.ToString(),
                res = true
            });
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> Signin([FromBody]LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            login.PassWord = Sha256(login.PassWord);
            var acc = await context.Accounts.Where(x => x.UserName == login.UserName && x.Password == login.PassWord).FirstOrDefaultAsync();
            if (acc == null)
            {
                return NotFound(new
                {
                    Name = "Not Found",
                    res = false
                });
            }
            string name = string.Empty;
            if (acc.IsAdmin)
            {
                session.AdminToken = Guid.NewGuid();
                name = acc.UserName + ";" + acc.ID + ";T;" + session.AdminToken.ToString();
            }
            else name = acc.UserName + ";" + acc.ID + ";F";
            await session.Set(acc.ID);
            return Ok(new
            {
                Name = name,
                res = await session.Get(acc.ID)
            });
        }

        [HttpGet, Route("session/{id}")]
        public async Task<IActionResult> CheckSession(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ID = Guid.Parse(id);
            var name = await context.Accounts.FindAsync(ID);
            return Ok(new
            {
                Name = name.UserName,
                res = await session.Get(ID)
            });
        }
        [HttpGet, Route("ClearSession/{id}")]
        public async Task<IActionResult> ClearSession(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ID = Guid.Parse(id);
            return Ok(new
            {
                Name = string.Empty,
                res = await session.Clear(ID)
            });
        }

        [HttpGet, Route("cities")]
        public IEnumerable<CityName> Cities()
        {
            return context.Cities;
        }
        private string Sha256(string input)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(input);
            string res = string.Empty;
            using (var sha256 = SHA256.Create())
            {
                res = Convert.ToBase64String(sha256.ComputeHash(data));
            }
            return res;
        }
    }
}