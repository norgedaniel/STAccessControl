using STCA_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STCA_ServiceLayer.TipoAreaAccesoList;

namespace STCA_ServiceLayer
{
    /// <summary>
    /// Class for easy query of TipoAreaAcceso entity offering facilities for ordering and filtering the data.
    /// To do this the methods development for that goal, must be able to be part of a LINQ query.
    /// The methods should return a IQueryable<TipoAreaAccesoDTO> receiving a parameter of type (this IQueryable<TipoAreaAcceso>)
    /// </summary>
    public static class TipoAreaAccesoList
    {
        public enum TipoAreaAccesoOrderByOptions
        {
            [StringValue("Reciente ↑")]
            DefaultOrder,

            [StringValue("Nombre ↑")]
            NombreAsc,

            [StringValue("Nombre ↓")]
            NombreDesc

        }

        /// <summary>
        /// Method to map TipoAreaAcceso entity into the TipoAreaAccesoDTO.
        /// The method uses the Query Object pattern; 
        /// the method takes in IQueryable<T> and outputs IQueryable<T>, which allows you to encapsulate a query, or part of a query, in a method.
        /// We’ll use the Query Object pattern for the sort, filter, and paging parts of the query too.
        /// 
        /// The method is also what .NET calls an extension method. Extension methods allow you to chain Query Objects together. 
        /// 
        /// A method can become an extension method if:
        ///  1- it’s declared in a static class, 
        ///  2- the method is static, and 
        ///  3- the first parameter has the keyword this in front of it.
        ///  
        /// </summary>
        public static IQueryable<TipoAreaAccesoDTO> MapTipoAreaAccesoDTO(this IQueryable<TipoAreaAcceso> tiposAreasAcceso)
        {
            return tiposAreasAcceso.Select(a => new TipoAreaAccesoDTO
            {
                TipoAreaAccesoId = a.TipoAreaAccesoId,
                Nombre = a.Nombre
            });
        }


        /// <summary>
        /// This extension method is to order a list of TipoAreaAccesoDTO as part of a LINQ query.
        /// Sorting in LINQ is done by the methods OrderBy and OrderByDescending. 
        /// Whith a method like this, we create a Query Object Query Object.
        /// In adition of the IQueryable<TipoAreaAccesoDTO> parameter we need another parameter to set the kind of orderby to apply.
        /// 
        /// Calling the OrderBooksBy method returns the original query with the appropriate LINQ sort command added to the end.
        /// We pass this query on to the next Query Object, or if you’ve finished, you call a command to execute the code, such as ToList.
        /// Even if the user doesn’t select a sort, you’ll still sort (see the Simple Order switch statement) because we’ll be using paging, 
        /// providing only a page at a time rather than all the data, and SQL requires the data to be sorted to handle paging.
        /// The most efficient sort is on the primary key, so we sort on that key.
        /// </summary>
        public static IQueryable<TipoAreaAccesoDTO> OrderTipoAreaAccesoBy(this IQueryable<TipoAreaAccesoDTO> tiposAreasAcceso,
                                                                              TipoAreaAccesoOrderByOptions orderByOptions = TipoAreaAccesoOrderByOptions.DefaultOrder)
        {
            switch (orderByOptions)
            {
                case TipoAreaAccesoOrderByOptions.DefaultOrder:
                    return tiposAreasAcceso.OrderByDescending(a => a.TipoAreaAccesoId);

                case TipoAreaAccesoOrderByOptions.NombreAsc:
                    return tiposAreasAcceso.OrderBy(a => a.Nombre);

                case TipoAreaAccesoOrderByOptions.NombreDesc:
                    return tiposAreasAcceso.OrderByDescending(a => a.Nombre);

                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }

        /// <summary>
        /// This extension method is to filter a list of TipoAreaAccesoDTO as part of a LINQ query.
        /// In this case we are implementing a filter over the Nombre field, type of string containing a substring sent as paramater.
        /// </summary>
        public static IQueryable<TipoAreaAccesoDTO> FilterTipoAreaAccesoBy(this IQueryable<TipoAreaAccesoDTO> tiposAreasAcceso,
                                                                               string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                // if no filterValue; return the original list
                return tiposAreasAcceso;

            return tiposAreasAcceso.Where(a => a.Nombre.Contains(filterValue));

        }

    }
}
