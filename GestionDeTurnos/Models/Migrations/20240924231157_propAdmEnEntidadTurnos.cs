using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class propAdmEnEntidadTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministradorId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_AdministradorId",
                table: "Turnos",
                column: "AdministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Administradores_AdministradorId",
                table: "Turnos",
                column: "AdministradorId",
                principalTable: "Administradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Administradores_AdministradorId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_AdministradorId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "AdministradorId",
                table: "Turnos");
        }
    }
}
