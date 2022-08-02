using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLayer
{

    /// <summary>
    /// Clase para implementar métodos extension a la clase ModelBuilder del EF Core.
    /// Para desarrollar un método extension para una clase; se debe implementar una clase estática, pública
    /// donde se programe el método extension, que debe ser estático, void y debe recibir como parámetro
    /// un objeto de la clase que se quiere extender. Este parámetro debe estar precedido por la palabra this.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoAreaAcceso>().HasData(
                new { TipoAreaAccesoId = 1, Nombre = "Oficina" },
                new { TipoAreaAccesoId = 2, Nombre = "Estacionamiento" },
                new { TipoAreaAccesoId = 3, Nombre = "Area Deportiva" },
                new { TipoAreaAccesoId = 4, Nombre = "Edificio Principal" },
                new { TipoAreaAccesoId = 5, Nombre = "Area Administrativa" },
                new { TipoAreaAccesoId = 6, Nombre = "Tipo de Area 1" },
                new { TipoAreaAccesoId = 7, Nombre = "Tipo de Area 2" },
                new { TipoAreaAccesoId = 8, Nombre = "Tipo de Area 3" },
                new { TipoAreaAccesoId = 9, Nombre = "Tipo de Area 4" },
                new { TipoAreaAccesoId = 10, Nombre = "Tipo de Area 5" },
                new { TipoAreaAccesoId = 11, Nombre = "Tipo de Area 6" },
                new { TipoAreaAccesoId = 12, Nombre = "Tipo de Area 7" },
                new { TipoAreaAccesoId = 13, Nombre = "Tipo de Area 8" },
                new { TipoAreaAccesoId = 14, Nombre = "Tipo de Area 9" },
                new { TipoAreaAccesoId = 15, Nombre = "Tipo de Area 10" },
                new { TipoAreaAccesoId = 16, Nombre = "Tipo de Area 11" },
                new { TipoAreaAccesoId = 17, Nombre = "Tipo de Area 12" },
                new { TipoAreaAccesoId = 18, Nombre = "Tipo de Area 13" },
                new { TipoAreaAccesoId = 19, Nombre = "Tipo de Area 14" },
                new { TipoAreaAccesoId = 20, Nombre = "Tipo de Area 15" }
                );
        }
    }
}
