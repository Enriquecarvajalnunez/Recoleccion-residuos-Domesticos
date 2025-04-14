using Microsoft.AspNetCore.Mvc;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaRecolectoraController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public EmpresaRecolectoraController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var empresas = _contenedorTrabajo.EmpresaRecolectora.GetAll();
            return Ok(empresas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var empresa = _contenedorTrabajo.EmpresaRecolectora.GetFirstOrDefault(e => e.IDEmpresa == id);
            if (empresa == null)
            {
                return NotFound(new { mensaje = $"No se encontro la empresa con el ID {id}" });
            }
            return Ok(empresa);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EmpresaRecolectora empresaRecolectora)
        {
            if (empresaRecolectora == null)
        {
            return BadRequest("La empresa recolectora enviada es nula");
        }
            _contenedorTrabajo.EmpresaRecolectora.Add(empresaRecolectora);
            _contenedorTrabajo.Save();
            return CreatedAtAction(nameof(GetById), new { id = empresaRecolectora.IDEmpresa }, empresaRecolectora);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmpresaRecolectora empresaRecolectora)
        {
            var empresaDesdeDb = _contenedorTrabajo.EmpresaRecolectora.GetFirstOrDefault(e => e.IDEmpresa == id);
            if (empresaDesdeDb == null)
            {
                return NotFound( new { mensaje = $"No se encontro la empresa con el ID {id}" });
            }
            empresaDesdeDb.Nombre = empresaRecolectora.Nombre;
            empresaDesdeDb.TipoResiduo = empresaRecolectora.TipoResiduo;
            _contenedorTrabajo.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var empresaDesdeDb = _contenedorTrabajo.EmpresaRecolectora.GetFirstOrDefault(e => e.IDEmpresa == id);
            if (empresaDesdeDb == null)
            {
                return NotFound(new { mensaje = $"No se encontro la empresa con el ID {id}" });
            }
            _contenedorTrabajo.EmpresaRecolectora.Remove(empresaDesdeDb);
            _contenedorTrabajo.Save();
            return NoContent();
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.