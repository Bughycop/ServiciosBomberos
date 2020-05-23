using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiciosBomberos.Web.Migrations
{
    public partial class ModifySalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prioridad",
                table: "Tipos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tipos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "Bombero1",
                table: "Salidas",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bombero2",
                table: "Salidas",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DiaSalida",
                table: "Salidas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "EsReten1",
                table: "Salidas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsReten2",
                table: "Salidas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bombero1",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Bombero2",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "DiaSalida",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "EsReten1",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "EsReten2",
                table: "Salidas");

            migrationBuilder.AlterColumn<string>(
                name: "Prioridad",
                table: "Tipos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tipos",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
