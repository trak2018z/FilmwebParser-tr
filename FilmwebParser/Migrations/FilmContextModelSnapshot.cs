using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FilmwebParser.Models;

namespace FilmwebParser.Migrations
{
    [DbContext(typeof(FilmContext))]
    partial class FilmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmwebParser.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cover");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Link");

                    b.Property<string>("Title");

                    b.Property<string>("UserName");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });
        }
    }
}
