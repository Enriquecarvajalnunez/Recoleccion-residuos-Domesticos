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
        public IActionResult GetAll()
        {
            var localidades = _contenedorTrabajo.Localidad.GetAll();
            return Ok(localidades);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Localidad.Add(localidad);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Localidad creada correctamente" });
            }
            return BadRequest(new { mensaje = "Error al crear la localidad" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Localidad.Update(localidad);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Localidad actualizada correctamente" });
            }
            return BadRequest(new { mensaje = "Error al actualizar la localidad" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var localidad = _contenedorTrabajo.Localidad.GetFirstOrDefault(l => l.IDLocalidad == id);
            if (localidad != null)
            {
                _contenedorTrabajo.Localidad.Remove(localidad);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Localidad eliminada correctamente" });
            }
            return NotFound(new { mensaje = $"Localidad no encontrada" });
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.