using Microsoft.EntityFrameworkCore.Migrations;

namespace STCA_DataLayer.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TiposAreasAcceso",
                columns: new[] { "TipoAreaAccesoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Oficina" },
                    { 18, "Tipo de Area 13" },
                    { 17, "Tipo de Area 12" },
                    { 16, "Tipo de Area 11" },
                    { 15, "Tipo de Area 10" },
                    { 14, "Tipo de Area 9" },
                    { 13, "Tipo de Area 8" },
                    { 12, "Tipo de Area 7" },
                    { 11, "Tipo de Area 6" },
                    { 10, "Tipo de Area 5" },
                    { 9, "Tipo de Area 4" },
                    { 8, "Tipo de Area 3" },
                    { 7, "Tipo de Area 2" },
                    { 6, "Tipo de Area 1" },
                    { 5, "Area Administrativa" },
                    { 4, "Edificio Principal" },
                    { 3, "Area Deportiva" },
                    { 2, "Estacionamiento" },
                    { 19, "Tipo de Area 14" },
                    { 20, "Tipo de Area 15" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TiposAreasAcceso",
                keyColumn: "TipoAreaAccesoId",
                keyValue: 20);
        }
    }
}
