using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecolectarController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public RecolectarController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        // Obtenemos todas las recolecciones.
        [HttpGet]
        public IActionResult GetAll()
        {
            var recolecciones = _contenedorTrabajo.Recolectar.GetAll();
            return Ok(recolecciones);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var recoleccion = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
            if (recoleccion == null)
            {
                return NotFound(new { mensaje = $"No se encontro la recoleccion con ID {id}" });
            }
            return Ok(recoleccion);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Recolectar recolectar)
        {
            if (recolectar.PesoKg <= 0)
            {
                return BadRequest(new { mensaje = "El peso en kg debe ser mayor a 0" });
            }
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Recolectar.Add(recolectar);
                _contenedorTrabajo.Save();
                return CreatedAtAction(nameof(GetById), new { id = recolectar.IDRecoleccion }, new { mensaje = "Recoleccion creada correctamente" });
            }
            return BadRequest(new { mensaje = "Error al crear la recoleccion" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Recolectar recolectar)
        {
            var recolectarDesdeDb = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
            if (recolectarDesdeDb == null)
            {
                return NotFound(new { mensaje = "No se encontro la recoleccion con el ID {id}" });
            }
            // Acutalizamos los valores de la recolección
            recolectarDesdeDb.IDUsuario = recolectar.IDUsuario;
            recolectarDesdeDb.IDEmpresa = recolectar.IDEmpresa;
            recolectarDesdeDb.IDResiduo = recolectar.IDResiduo;
            recolectarDesdeDb.FechaRecoleccion = recolectar.FechaRecoleccion;
            recolectarDesdeDb.PesoKg = recolectar.PesoKg;
            recolectarDesdeDb.Estado = recolectar.Estado;

            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "Recoleccion actualizada correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var recolectarDesdeDb = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
            if (recolectarDesdeDb == null)
            {
                return NotFound(new { mensaje = "No se encontro la recoleccion con el ID {id}" });
            }
            _contenedorTrabajo.Recolectar.Remove(recolectarDesdeDb);
            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "Recoleccion eliminada correctamente" });
        }
    }
}
// GetAll: Obtenemos todas las recolecciones.
// GetById: Obtenemos una recolección por su ID.
// Create: Creamos una nueva recolección.
// Update: Actualizamos una recolección existente.
// Delete: Eliminamos una recolección especificada por su ID.