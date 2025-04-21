using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTipoResiduoController : Controller
    {
        public readonly IContenedorTrabajo _contenedorTrabajo;

        public SubTipoResiduoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult GetAllSubTipoResiduo()
        {
            var subTipoResiduo = _contenedorTrabajo.ObtenerTodosLosSubTipoResiduos();
            if (subTipoResiduo == null || !subTipoResiduo.Any())
            {
                return NotFound("No se encontraron subtipos de residuo.");
            }
            return Ok(subTipoResiduo);
        }
    }
}
