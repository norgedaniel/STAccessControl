using Microsoft.EntityFrameworkCore;
using STCA_DataLayer;
using STCA_ServiceLayer;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            ListTiposAreasAcceso();
        }

        public static void ListTiposAreasAcceso()
        {
            using (var db = new AppDbContext())
            {
                //var lista = db.TiposAreasAcceso.AsNoTracking().ToList();

                TiposAreasAccesoListService service = new TiposAreasAccesoListService(db);

                TiposAreasAccesoList tiposAreasAccesoList = service.SortFilterPage(TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.NombreAsc, 12, 8, "ipo");

                if (tiposAreasAccesoList.PagesCount == 0)
                    return;

                foreach (var item in tiposAreasAccesoList.TipoAreaAccesoDTOList)
                {
                    Console.WriteLine($"Id TipoAreaAcceso: {item.TipoAreaAccesoId}, Nombre: {item.Nombre}");
                }

            }
        }
    }
}
