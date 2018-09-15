using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Mime;
using InternetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Coravel;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace InternetProject.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public SessionProvider Session = new SessionProvider();
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
            services.AddSingleton<SessionProvider>(Session);
            services.AddScheduler(scheduler =>
            {
                scheduler.Schedule(() => Session.Clear())
                .EveryFiveMinutes();
            });
            services.AddScheduler(scheduler =>
            {
                scheduler.Schedule(() => 
                    {
                        var option = new DbContextOptionsBuilder<Context>();
                        var options = option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options;
                        using (var context=new Context(options))
                        {
                            var r = context.Ads.Where(x => x.AdTime < DateTime.Now.AddDays(-7)).ToList();
                            foreach (var item in r)
                            {
                                item.Expired = true;
                                context.Entry(item).State = EntityState.Modified;
                            }
                            context.Ads.UpdateRange(r);
                            context.SaveChanges();
                        }
                    })
                .Weekly();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/upload", c =>
            {
                c.Run(async context =>
                {
                    string bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    var s = bodyAsText.Split(';');
                    var ss = s.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToList();
                    StringBuilder sb = new StringBuilder();
                    var db = context.RequestServices.GetRequiredService<Context>();
                    foreach (var item in ss)
                    {
                        var add = Guid.NewGuid().ToString();
                        var path = Path.Combine(env.ContentRootPath, "Img", add);
                        await System.IO.File.WriteAllTextAsync(path, item);
                        var img = new Image()
                        {
                            ID = Guid.NewGuid(),
                            Name = add
                        };
                        db.Entry(img).State = EntityState.Added;
                        db.Images.Add(img);
                        sb.Append(img.ID.ToString());
                        sb.Append(';');
                    }
                    await db.SaveChangesAsync();
                    context.Response.ContentType = "application/json";
                    context.Response.Headers.Add("file", sb.ToString());
                    await context.Response.WriteAsync("True");
                });
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
            app.UseBlazor<Client.Program>();
        }
    }
}
