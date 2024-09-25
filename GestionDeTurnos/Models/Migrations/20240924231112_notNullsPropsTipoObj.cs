using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class notNullsPropsTipoObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canchas_Deportes_DeporteId",
                table: "Canchas");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementosCancha_Canchas_CanchaId",
                table: "ElementosCancha");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementosCancha_Elementos_ElementoId",
                table: "ElementosCancha");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Administradores_AdminId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Canchas_CanchaId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Clientes_ClienteId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_AdminId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Turnos");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ElementoId",
                table: "ElementosCancha",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "ElementosCancha",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Canchas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Canchas_Deportes_DeporteId",
                table: "Canchas",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementosCancha_Canchas_CanchaId",
                table: "ElementosCancha",
                column: "CanchaId",
                principalTable: "Canchas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementosCancha_Elementos_ElementoId",
                table: "ElementosCancha",
                column: "ElementoId",
                principalTable: "Elementos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Canchas_CanchaId",
                table: "Turnos",
                column: "CanchaId",
                principalTable: "Canchas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Clientes_ClienteId",
                table: "Turnos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canchas_Deportes_DeporteId",
                table: "Canchas");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementosCancha_Canchas_CanchaId",
                table: "ElementosCancha");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementosCancha_Elementos_ElementoId",
                table: "ElementosCancha");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Canchas_CanchaId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Clientes_ClienteId",
                table: "Turnos");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Turnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "Turnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ElementoId",
                table: "ElementosCancha",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "ElementosCancha",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Canchas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_AdminId",
                table: "Turnos",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canchas_Deportes_DeporteId",
                table: "Canchas",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementosCancha_Canchas_CanchaId",
                table: "ElementosCancha",
                column: "CanchaId",
                principalTable: "Canchas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementosCancha_Elementos_ElementoId",
                table: "ElementosCancha",
                column: "ElementoId",
                principalTable: "Elementos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Administradores_AdminId",
                table: "Turnos",
                column: "AdminId",
                principalTable: "Administradores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Canchas_CanchaId",
                table: "Turnos",
                column: "CanchaId",
                principalTable: "Canchas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Clientes_ClienteId",
                table: "Turnos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
