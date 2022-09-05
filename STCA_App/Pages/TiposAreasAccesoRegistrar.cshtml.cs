using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using STCA_ServiceLayer;


/*
TODO:

1- validaciones

2- tratamiento de error. cómo se muestra un error cuando ocurre:
    - error al buscar.
    - error al guardar.
    - error al duplicar un nombre. DONE
 
 */


namespace STCA_App.Pages
{
    public class TiposAreasAccesoRegistrarModel : PageModel
    {
        // class service to get access to the TiposAreasAcceso entities.
        private readonly TiposAreasAccesoListService _service;



        // variables for binding process

        [BindProperty]
        public TipoAreaAccesoDTO Input { get; set; } = new TipoAreaAccesoDTO();

        public TiposAreasAccesoRegistrarModel(TiposAreasAccesoListService service)
        {
            _service = service;
        }


        public IActionResult OnGet(int id)
        {
            if (id <= 0)
                return Page();   // request for add a record

            // buscar los datos del registro de TipoAreaAcceso solicitado
            var record = _service.Find(id);

            if (record == null)
                // registro no encontrado, retornar a la página principal que muestra los registros en forma paginada
                // TODO: mostrar de alguna manera que el registro no fue encontrado.
                return RedirectToPage("TiposAreasAcceso");

            // mostrar datos del registro encontrado
            Input = record;

            return Page();

        }

        public IActionResult OnPost(int id)
        {

            if (!ModelState.IsValid)
                return Page();

            // since there is no hidden input to hold the TipoAreaAccesoId
            // we need to update our Input object with the id which come from our model binding.
            Input.TipoAreaAccesoId = id;

            try
            {
                // guardar los datos
                _service.SaveRecord(Input);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                {
                    ModelState.AddModelError(string.Empty, "El registro ya no está disponible en la base de datos.");
                    return Page();
                }

                if (ex.InnerException != null && ex.InnerException.Message.ToLower().Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "El Nombre introducido ya está registrado.");
                    return Page();
                }
                throw;
            }

            // ir a mostrar la página principal que muestra los registros en forma paginada
            return RedirectToPage("TiposAreasAcceso");

        }


    }
}
