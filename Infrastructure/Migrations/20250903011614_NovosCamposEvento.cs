using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NovosCamposEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Eventos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtEvento",
                table: "Eventos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "DtEvento",
                table: "Eventos");
        }
    }
}
