using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResiduoController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ResiduoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet("tipos-residuo")]
        public IActionResult GetTiposResiduo()
        {
            var tiposResiduos = new List<string>
            {
                "Orgánico",
                "Inorgánico Reciclable",
                "Peligroso"
            };
            return Ok(tiposResiduos);
        }
    }
}
