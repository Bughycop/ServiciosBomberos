using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiciosBomberos.Web.Migrations
{
    public partial class NewModifiSalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_TipoSalidaId",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "TipoSalidaId",
                table: "Salidas");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Salidas",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoSalida",
                table: "Salidas",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoSalida",
                table: "Salidas");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Salidas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "TipoSalidaId",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_TipoSalidaId",
                table: "Salidas",
                column: "TipoSalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Tipos_TipoSalidaId",
                table: "Salidas",
                column: "TipoSalidaId",
                principalTable: "Tipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
