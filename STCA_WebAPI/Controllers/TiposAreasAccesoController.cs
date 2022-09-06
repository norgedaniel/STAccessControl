using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCA_ServiceLayer;

namespace STCA_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TiposAreasAccesoController : ControllerBase
    {
        // class service to get access to the TiposAreasAcceso entities.
        private readonly TiposAreasAccesoListService _service;

        public TiposAreasAccesoController(TiposAreasAccesoListService service)
        {
            _service = service;
            _service.SeedData();
        }

        [HttpGet(Name = "GetTiposAreasAccesoList")]
        public ActionResult<TiposAreasAccesoList> GetTiposAreasAccesoList(
                TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions ordenDatos
                        = TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.DefaultOrder,
                int longitudPagina = 10, int pagenumber = 1, string? filtroNombre = "")
        {
            TiposAreasAccesoList tiposAreasAccesoList = _service.SortFilterPage(ordenDatos, longitudPagina, pagenumber, filtroNombre);

            return Ok(tiposAreasAccesoList);

        }

        [HttpGet("GetTipoAreaAcceso/{id:int}")]
        public ActionResult<TipoAreaAccesoDTO> Find(int id)
        {
            TipoAreaAccesoDTO? tipoAreaAccesoDTO = _service.Find(id);

            if (tipoAreaAccesoDTO == null)
                return NotFound();

            return Ok(tipoAreaAccesoDTO);

        }

    }
}
