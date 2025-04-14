using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi.Recoleccion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionPuntosController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ConfiguracionPuntosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] ConfiguracionPuntos configuracionPunto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ConfiguracionPuntos.Add(configuracionPunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}