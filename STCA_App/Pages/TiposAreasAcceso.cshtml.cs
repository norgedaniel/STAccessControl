using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCA_DataLayer;
using STCA_ServiceLayer;
using static STCA_ServiceLayer.TipoAreaAccesoList;

namespace STCA_App.Pages
{
    public class TiposAreasAccesoModel : PageModel
    {

        private readonly TiposAreasAccesoListService _service;

        public List<TipoAreaAccesoDTO> ListTiposAreasAcceso;

        // object for binding model in this razor page
        [BindProperty]
        public TiposAreasAccesoModelBinding Input { get; set; }

        // lista de opciones a mostrar para establecer el orden de los datos 
        // el tag helper relacionado con Select, necesita trabajar con una lista de SelectListItem
        public IEnumerable<SelectListItem> OrdenDatosItems { get; set; }
            = new List<SelectListItem>
            {
                new SelectListItem{Value= TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.DefaultOrder.ToString(),
                                   Text=TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.DefaultOrder.GetStringValue()},
                
                new SelectListItem{Value= TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.NombreAsc.ToString(), 
                                   Text=TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.NombreAsc.GetStringValue()},
                
                new SelectListItem{Value= TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.NombreDesc.ToString(), 
                                   Text=TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.NombreDesc.GetStringValue()}

            };

        public TiposAreasAccesoModel(TiposAreasAccesoListService service)
        {
            ListTiposAreasAcceso = new List<TipoAreaAccesoDTO>();
            _service = service;

            Input = new TiposAreasAccesoModelBinding();

        }

        public void OnGet()
        {
            RefreshData();
        }

        public void OnPost()
        {
            RefreshData();
        }

        private void RefreshData()
        {

            TipoAreaAccesoList.TipoAreaAccesoOrderByOptions ordenDatos 
                = (TipoAreaAccesoList.TipoAreaAccesoOrderByOptions)Enum.Parse(typeof(TipoAreaAccesoList.TipoAreaAccesoOrderByOptions), Input.OrdenDatos);

            ListTiposAreasAcceso = _service.SortFilterPage(ordenDatos, Input.FiltroNombre, 1, 5).ToList();

        }

    }

    // nested class for model binding
    public class TiposAreasAccesoModelBinding
    {
        public string FiltroNombre { get; set; }

        public string OrdenDatos { get; set; }

        public TiposAreasAccesoModelBinding()
        {
            FiltroNombre = "";
            OrdenDatos = TipoAreaAccesoList.TipoAreaAccesoOrderByOptions.DefaultOrder.ToString();
        }

    }

}
