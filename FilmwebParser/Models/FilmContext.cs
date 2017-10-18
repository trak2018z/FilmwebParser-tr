using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FilmwebParser.Models
{
    public class FilmContext : DbContext
    {
        private IConfigurationRoot _config;
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmDetail> FilmDetails { get; set; }

        public FilmContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:FilmContextConnection"]);
        }
    }
}
