using Microsoft.EntityFrameworkCore;
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
    /// This class implements a complex query to list TiposAreasAcceso entities. 
    /// The class allows the user to arrange, filter and pagine the list. 
    /// It combines the extension methods of the TipoAreaAccesoList to form the complex query.
    /// 
    /// The class has only the method SortFilterPage to build the composite query. 
    /// It needs the Application DBContext which is supplied by as a parameter via Constructor.
    /// </summary>
    public class TiposAreasAccesoListService
    {
        private AppDbContext _context;

        public TiposAreasAccesoListService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<TipoAreaAccesoDTO> SortFilterPage(TipoAreaAccesoOrderByOptions orderBy, string filterValue,
                                                            int pageNumber, int pageSize)
        {
            var tiposAreasAccesoQuery = _context.TiposAreasAcceso
                .AsNoTracking()
                .MapTipoAreaAccesoDTO() // this method converts from TipoAreaAcceso into TipoAreaAccesoDTO
                .OrderTipoAreaAccesoBy(orderBy)
                .FilterTipoAreaAccesoBy(filterValue);

            return tiposAreasAccesoQuery.Page(pageNumber - 1, pageSize);

        }

    }
}
