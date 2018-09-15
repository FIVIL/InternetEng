using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InternetProject.ViewModels;
using System.Net.Mail;
using System.Net;

namespace InternetProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost,Route("Mail")]
        public async Task Mail([FromBody] MailViewModel MVM)
        {
            await Task.Run(() =>
            {
               // try
              //  {
                    using (var client = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = _configuration["Email:Email"],
                            Password = _configuration["Email:Password"]
                        };

                        client.Credentials = credential;
                        client.Host = _configuration["Email:Host"];
                        client.Port = int.Parse(_configuration["Email:Port"]);
                        client.EnableSsl = true;
                        using (var emailMessage = new MailMessage())
                        {
                            emailMessage.To.Add(new MailAddress(MVM.Address));
                            emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                            emailMessage.Subject = "Car Info";
                            emailMessage.Body = $"<h1><i>Asayesh khodro....</i></h1><hr />Please check <a href=\"{MVM.Uri}\">this link</a>.";
                            emailMessage.IsBodyHtml = true;
                            client.Send(emailMessage);
                        }
                    }
               // }
                //catch {}
            });
        }
    }
}