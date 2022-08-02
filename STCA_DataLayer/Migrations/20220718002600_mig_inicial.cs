using Microsoft.EntityFrameworkCore.Migrations;

namespace STCA_DataLayer.Migrations
{
    public partial class mig_inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposAreasAcceso",
                columns: table => new
                {
                    TipoAreaAccesoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAreasAcceso", x => x.TipoAreaAccesoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposAreasAcceso");
        }
    }
}
