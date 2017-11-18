using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmwebParser.Migrations
{
    public partial class UpdateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Films");

            migrationBuilder.AddColumn<string>(
                name: "Cast",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Screenplay",
                table: "Films",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cast",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Screenplay",
                table: "Films");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Films",
                nullable: false,
                defaultValue: 0);
        }
    }
}
