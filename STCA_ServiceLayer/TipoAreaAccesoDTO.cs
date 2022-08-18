using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_ServiceLayer
{
    /// <summary>
    /// This class known as a Data Transformation Object is oriented to hold exactly the data we expect to get
    /// when querying the TipoAreaAcceso entity. This is a simple example but some time it´s posible to get
    /// more complex properties as calculated data.
    /// To work with EF Core’s select loading, the class that’s going to receive the data must have a default constructor 
    /// (which you can create without providing any properties to the constructor), the class must not be static, 
    /// and the properties must have public setters.
    /// </summary>
    public class TipoAreaAccesoDTO
    {
        public int TipoAreaAccesoId { get; set; }
        public string Nombre { get; set; } = "";

    }
}
