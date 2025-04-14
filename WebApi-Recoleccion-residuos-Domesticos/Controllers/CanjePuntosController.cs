using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanjePuntosController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CanjePuntosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var canjes = _contenedorTrabajo.CanjePuntos.GetAll();
            return Ok(canjes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var canje = _contenedorTrabajo.CanjePuntos.GetFirstOrDefault(c => c.IDCanje == id);
            if (canje == null)
            {
                return NotFound(new { mensaje = $"No se encontro el canje con el ID {id}" });
            }
            return Ok(canje);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] CanjePuntos canjePuntos)
        {
            if (canjePuntos.PuntosUsados <= 0) // Validamos los puntos.
            {
                return BadRequest(new { mensaje = "Los puntos usados deben ser mayores a 0" });
            }

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.CanjePuntos.Add(canjePuntos);
                _contenedorTrabajo.Save();
                return CreatedAtAction(nameof(GetById), new { id = canjePuntos.IDCanje }, new { mensaje = "Canje creado correctamente" });
            }
            return BadRequest(new { mensaje = "Error al crear el canje" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CanjePuntos canjePuntos)
        {
            var canjeDesdeDb = _contenedorTrabajo.CanjePuntos.GetFirstOrDefault(c => c.IDCanje == id);
            if (canjeDesdeDb == null)
            {
                return NotFound(new { mensaje = $"No se encontro el canje con el ID {id}" });
            }

            // Actualizamos los valores.
            canjeDesdeDb.IDUsuario = canjePuntos.IDUsuario;
            canjeDesdeDb.PuntosUsados = canjePuntos.PuntosUsados;
            canjeDesdeDb.Tienda = canjePuntos.Tienda;

            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "Canje actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var canjeDesdeDb = _contenedorTrabajo.CanjePuntos.GetFirstOrDefault(c => c.IDCanje == id);
            if (canjeDesdeDb == null)
            {
                return NotFound(new { mensaje = $"No se encontro el canje con el ID {id}" });
            }
            _contenedorTrabajo.CanjePuntos.Remove(canjeDesdeDb);
            _contenedorTrabajo.Save();

            return Ok(new { mensaje = "Canje eliminado correctamente" });
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.