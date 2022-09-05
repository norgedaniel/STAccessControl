using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLayer
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class TipoAreaAcceso
    {
        public int TipoAreaAccesoId { get; set; }
        public string Nombre { get; set; }
    }
}
