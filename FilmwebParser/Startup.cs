using AutoMapper;
using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace FilmwebParser
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");
            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            if (_env.IsEnvironment("Development"))
                services.AddScoped<IMailService, DebugMailService>();
            services.AddScoped<IParserService, FilmParserService>();
            services.AddDbContext<FilmContext>();
            services.AddTransient<ParseLinkResult>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddTransient<FilmContextSeedData>();
            services
                .AddMvc(config =>
                {
                    if (_env.IsProduction())
                        config.Filters.Add(new RequireHttpsAttribute());
                })
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddIdentity<FilmUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            ctx.Response.StatusCode = 401;
                        else
                            ctx.Response.Redirect(ctx.RedirectUri);
                        await Task.Yield();
                    }
                };
            }).AddEntityFrameworkStores<FilmContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FilmContextSeedData seeder)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<FilmViewModel, Film>().ReverseMap();
            });
            if (env.IsEnvironment("Development"))
                app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
            //seeder.EnsureSeedData().Wait();
        }
    }
}
