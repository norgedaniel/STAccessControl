using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STCA_DataLayer.Migrations
{
    public partial class uniq_index_TipoAreaAcceso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TiposAreasAcceso",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposAreasAcceso_Nombre",
                table: "TiposAreasAcceso",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TiposAreasAcceso_Nombre",
                table: "TiposAreasAcceso");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TiposAreasAcceso",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
