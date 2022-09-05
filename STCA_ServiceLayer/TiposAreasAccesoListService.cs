using Microsoft.EntityFrameworkCore;
using STCA_DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static STCA_ServiceLayer.TipoAreaAccesoExtension;

namespace STCA_ServiceLayer
{
    /// <summary>
    /// Class Service as bridge between any Application layer and the database layer to work with the TiposAreasAcceso entities.
    /// This class implements a complex query to list TiposAreasAcceso entities allowing sort out, filters and paging over the list.
    /// It combines the extension methods of the TipoAreaAccesoList to form the complex query.
    /// 
    /// The class also provides the methods needed to do CRUD for TiposAreasAcceso entities.
    /// 
    /// It needs the Application DBContext which is supplied trhought Dependency Injection as a parameter via Constructor.
    /// </summary>
    public class TiposAreasAccesoListService
    {
        private AppDbContext _context;

        public TiposAreasAccesoListService(AppDbContext context) => _context = context;

        public TiposAreasAccesoList SortFilterPage(TipoAreaAccesoOrderByOptions orderBy, int pageSize, int pageNumber, string? filterValueNombre = null)
        {
            if (pageSize <= 0)
                return new TiposAreasAccesoList(); // incorrect requested page size 



            var tiposAreasAccesoQuery = _context.TiposAreasAcceso
                .AsNoTracking()
                .MapTipoAreaAccesoDTO() // this method converts from TipoAreaAcceso into TipoAreaAccesoDTO
                .OrderTipoAreaAccesoBy(orderBy)
                .FilterTipoAreaAccesoBy(filterValueNombre);

            if (tiposAreasAccesoQuery == null || tiposAreasAccesoQuery.Count() == 0)
                return new TiposAreasAccesoList(); // no data found



            if (pageNumber <= 0)
                pageNumber = 1;

            int pagesCount = (int)Math.Ceiling(tiposAreasAccesoQuery.Count() / (double)pageSize);

            // if PageNumber request is greather than the PagesCount; take PagesCount as the pageNumber
            pageNumber = Math.Min(pagesCount, pageNumber);

            List<TipoAreaAccesoDTO> lista = tiposAreasAccesoQuery.Page(pageNumber - 1, pageSize).ToList();

            if (lista == null || lista.Count() == 0)
                return new TiposAreasAccesoList(); // no data found


            // return the object with the data found
            return new TiposAreasAccesoList(lista, pagesCount, pageNumber);

        }

        public TipoAreaAccesoDTO? Find(int tipoAreaAccesoId)
        {
            return _context.TiposAreasAcceso
                .AsNoTracking()
                .Select(p => new TipoAreaAccesoDTO
                {
                    TipoAreaAccesoId = p.TipoAreaAccesoId,
                    Nombre = p.Nombre
                })
                .FirstOrDefault(a => a.TipoAreaAccesoId == tipoAreaAccesoId);

        }

        public void SaveRecord(TipoAreaAccesoDTO tipoAreaAccesoDTO)
        {
            if (tipoAreaAccesoDTO == null)
                throw new Exception("null TipoAreaAcceso requested for update.");

            if (tipoAreaAccesoDTO.TipoAreaAccesoId > 0)
            {
                // request for update some record
                TipoAreaAcceso? record = _context.TiposAreasAcceso
                     .FirstOrDefault(a => a.TipoAreaAccesoId == tipoAreaAccesoDTO.TipoAreaAccesoId);

                if (record == null)
                    throw new Exception("TipoAreaAcceso not found.");

                // updating the record fields
                record.Nombre = tipoAreaAccesoDTO.Nombre;

            }
            else
            {
                // request for add new record
                _context.TiposAreasAcceso.Add(new TipoAreaAcceso { Nombre = tipoAreaAccesoDTO.Nombre });
            }

            _context.SaveChanges();

        }

        public void DeleteRecord(int tipoAreaAccesoId)
        {
            if (tipoAreaAccesoId <= 0)
                throw new Exception("Invalid TipoAreaAcceso requested for delete.");

            TipoAreaAcceso? record = _context.TiposAreasAcceso
                 .FirstOrDefault(a => a.TipoAreaAccesoId == tipoAreaAccesoId);

            if (record == null || record.TipoAreaAccesoId <= 0)
                throw new Exception("TipoAreaAcceso not found.");

            _context.Remove(record);
            _context.SaveChanges();

        }

        public void SeedData()
        {
            try
            {
                if (_context.TiposAreasAcceso.Count() > 0)
                    return;

                _context.TiposAreasAcceso.AddRange(new TipoAreaAcceso[] {
                    new TipoAreaAcceso() {Nombre="Oficina"},
                    new TipoAreaAcceso() { Nombre = "Estacionamiento" },
                    new TipoAreaAcceso() { Nombre = "Area Deportiva" },
                    new TipoAreaAcceso() { Nombre = "Edificio Principal" },
                    new TipoAreaAcceso() { Nombre = "Area Administrativa" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 1" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 2" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 3" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 4" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 5" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 6" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 7" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 8" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 9" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 10" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 11" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 12" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 13" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 14" },
                    new TipoAreaAcceso() { Nombre = "Tipo de Area 15" }
                });
                _context.SaveChanges();
             }
            catch { }
        }

    }
}
