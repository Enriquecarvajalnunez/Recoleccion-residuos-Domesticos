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

        [HttpGet]
        public IActionResult GetAll()
        {
            var residuos = _contenedorTrabajo.Residuo.GetAll();
            return Ok(residuos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Residuo residuo)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Residuo.Add(residuo);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Residuo creado correctamente" });
            }
            return BadRequest(new { mensaje = "Error al crear el residuo" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Residuo residuo)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Residuo.Update(residuo);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Residuo actualizado correctamente" });
            }
            return BadRequest(new { mensaje = "Error al actualizar el residuo" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var residuo = _contenedorTrabajo.Residuo.GetFirstOrDefault(r => r.IDResiduo == id);
            if (residuo != null)
            {
                _contenedorTrabajo.Residuo.Remove(residuo);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Residuo eliminado correctamente" });
            }
            return NotFound(new { mensaje = "Residuo no encontrado" });
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.