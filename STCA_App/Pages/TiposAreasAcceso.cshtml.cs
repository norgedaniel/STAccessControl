using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STCA_DataLayer;
using STCA_ServiceLayer;

namespace STCA_App.Pages
{
    public class TiposAreasAccesoModel : PageModel
    {

        private readonly TiposAreasAccesoListService _service;

        public List<TipoAreaAccesoDTO> ListTiposAreasAcceso;

        public TiposAreasAccesoModel(TiposAreasAccesoListService service)
        {
            ListTiposAreasAcceso = new List<TipoAreaAccesoDTO>();
            _service = service;
        }

        public void OnGet()
        {
            ListTiposAreasAcceso = _service.SortFilterPage(TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.SimpleOrder,
                                                           "", 1, 5).ToList();
        }
    }
}
