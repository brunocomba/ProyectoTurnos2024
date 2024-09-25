using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class propADMenEntidadTurno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Canchas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_AdminId",
                table: "Turnos",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Administradores_AdminId",
                table: "Turnos",
                column: "AdminId",
                principalTable: "Administradores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Administradores_AdminId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_AdminId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Turnos");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Canchas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
