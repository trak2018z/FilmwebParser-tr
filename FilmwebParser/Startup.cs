using AutoMapper;
using FilmwebParser.Models;
using FilmwebParser.Services;
using FilmwebParser.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

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
            services.AddLogging();
            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FilmContextSeedData seeder, ILoggerFactory factory)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<FilmViewModel, Film>().ReverseMap();
            });
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
                factory.AddDebug(LogLevel.Error);
            app.UseStaticFiles();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
            seeder.EnsureSeedData().Wait();
        }
    }
}
