using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public LocalidadController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult GetAllLocalidades()
        {
            var localidades = _contenedorTrabajo.ObtenerTodasLasLocalidades();
            if (localidades == null || !localidades.Any())
            {
                return NotFound("No se encontraron localidades.");
            }
            return Ok(localidades);
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.