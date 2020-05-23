using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiciosBomberos.Web.Migrations
{
    public partial class ModifyTipoSalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas");

            migrationBuilder.AlterColumn<string>(
                name: "Prioridad",
                table: "Tipos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tipos",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoSalidaId",
                table: "Salidas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas",
                column: "TipoSalidaId",
                principalTable: "Tipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas");

            migrationBuilder.AlterColumn<string>(
                name: "Prioridad",
                table: "Tipos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tipos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<int>(
                name: "TipoSalidaId",
                table: "Salidas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas",
                column: "TipoSalidaId",
                principalTable: "Tipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
