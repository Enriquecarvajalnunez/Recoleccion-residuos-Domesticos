using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
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

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaConfigPunto = _contenedorTrabajo.ConfiguracionPunto.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaConfigPunto);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] ConfiguracionPunto configuracionPunto)
        {
            if (configuracionPunto == null) 
            {
                return BadRequest("Datos inválidos");
            }
            
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ConfiguracionPunto.Add(configuracionPunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Configurado Punto correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(ConfiguracionPunto configPunto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ConfiguracionPunto.Update(configPunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Punto Editado" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.ConfiguracionPunto.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.ConfiguracionPunto.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Configuración eliminada" });
        }
    }
}
