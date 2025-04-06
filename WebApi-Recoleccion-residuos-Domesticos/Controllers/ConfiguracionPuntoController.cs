using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;

namespace WebApi.Recoleccion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionPuntoController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ConfiguracionPuntoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] ConfiguracionPunto configuracionPunto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ConfiguracionPunto.Add(configuracionPunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}