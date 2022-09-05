using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_ServiceLayer
{
    public class TiposAreasAccesoList
    {
        public List<TipoAreaAccesoDTO> TipoAreaAccesoDTOList { get; }

        public int PagesCount { get; }

        public int PageNumber { get; }

        public TiposAreasAccesoList(List<TipoAreaAccesoDTO> tipoAreaAccesoDTOList, int pagesCount, int pageNumber)
        {
            TipoAreaAccesoDTOList = tipoAreaAccesoDTOList;
            PagesCount = pagesCount;
            PageNumber = pageNumber;
        }

        public TiposAreasAccesoList()
        {
            TipoAreaAccesoDTOList = new List<TipoAreaAccesoDTO>();
        }

    }
}
