using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCA_DataLayer;
using STCA_ServiceLayer;
using STCA_Tool;
using System.ComponentModel.DataAnnotations;
using static STCA_ServiceLayer.TipoAreaAccesoExtension;

/*
TODO:

1- cuando se invoca a la página de actualizar un registro, se debe guardar en el diccinario ViewData los criterios de selección
   Por tanto siempre que se entre desde el menú principal hay que resetear esos criterios de búsqueda y actualizarlos en el ViewData

 */


namespace STCA_App.Pages
{
    public class TiposAreasAccesoModel : PageModel
    {
        // class service to get access to the TiposAreasAcceso entities.
        private readonly TiposAreasAccesoListService _service;



        // variables for binding process

        [BindProperty]
        public TiposAreasAccesoModelBinding Input { get; set; } = new TiposAreasAccesoModelBinding();

        [BindProperty]
        public int pagenumber { get; set; }



        // public properties to be used by the view when rendering the HTML to the user.

        public TiposAreasAccesoList TiposAreasAccesoList { get; private set; } = new TiposAreasAccesoList();

        // lista de opciones a mostrar para establecer el orden de los datos 
        // el tag helper relacionado con Select, necesita trabajar con una lista de SelectListItem
        public IEnumerable<SelectListItem> OrdenDatosItems { get; set; }
            = new List<SelectListItem>
            {
                new SelectListItem{Value= TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.DefaultOrder.ToString(),
                                   Text=TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.DefaultOrder.GetStringValue()},

                new SelectListItem{Value= TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.NombreAsc.ToString(),
                                   Text=TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.NombreAsc.GetStringValue()},

                new SelectListItem{Value= TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.NombreDesc.ToString(),
                                   Text=TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.NombreDesc.GetStringValue()}

            };

        public IEnumerable<SelectListItem> PageLenghtItems { get; set; }
                = new List<SelectListItem>
                {
                    new SelectListItem{Value= "10", Text= "10"},
                    new SelectListItem{Value= "20", Text= "20"},
                    new SelectListItem{Value= "30", Text= "30"},
                    new SelectListItem{Value= "40", Text= "40"},
                    new SelectListItem{Value= "50", Text= "50"}
                };



        public TiposAreasAccesoModel(TiposAreasAccesoListService service)
        {
            _service = service;
            _service.SeedData();
        }
 
        public void OnGet()
        {
            RefreshData();
        }

        //public void OnGetDelete(int id)
        public void OnPostDelete(int id)
        {
            try
            {
                // guardar los datos
                _service.DeleteRecord(id);
                RefreshData();
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                    throw;

                if (!ex.Message.ToLower().Contains("Invalid") &&
                    !ex.Message.ToLower().Contains("not found"))
                    throw;

                RefreshData();

            }
        }

        public void OnPost()
        {
            RefreshData();
        }

        //public void OnPostPagina(int pagenumber)
        //{
        //    RefreshData(pagenumber);
        //}


        private void RefreshData()
        {

            TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions ordenDatos
                = (TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions)Enum.Parse(typeof(TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions), Input.OrdenDatos);

            TiposAreasAccesoList = _service.SortFilterPage(ordenDatos, Input.LongitudPagina, pagenumber, Input.FiltroNombre);

            pagenumber = TiposAreasAccesoList.PageNumber;

        }

    }

    // nested class for model binding
    public class TiposAreasAccesoModelBinding
    {
        [Display(Name = "Ordenar por:")]
        public string OrdenDatos { get; set; }

        [Display(Name = "Filtrar Nombre:")]
        public string FiltroNombre { get; set; }

        [Display(Name = "Tamaño de página:")]
        public int LongitudPagina { get; set; }

        public TiposAreasAccesoModelBinding()
        {
            OrdenDatos = TipoAreaAccesoExtension.TipoAreaAccesoOrderByOptions.DefaultOrder.ToString();
            FiltroNombre = "";
            LongitudPagina = 10;
        }

    }

}
