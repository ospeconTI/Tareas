using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                schema: "dbo",
                table: "Tarea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item",
                schema: "dbo",
                table: "Tarea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lapso",
                schema: "dbo",
                table: "Tarea",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                schema: "dbo",
                table: "Tarea");

            migrationBuilder.DropColumn(
                name: "Item",
                schema: "dbo",
                table: "Tarea");

            migrationBuilder.DropColumn(
                name: "Lapso",
                schema: "dbo",
                table: "Tarea");
        }
    }
}
