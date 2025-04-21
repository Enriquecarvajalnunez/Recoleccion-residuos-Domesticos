using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador")]
        public IActionResult Create([FromBody] EmpresaRecolectora empresaRecolectora)
        {
            if (empresaRecolectora == null || !ModelState.IsValid)
        {
            return BadRequest("La empresa recolectora enviada es nula");
        }

            // Validación para evitar duplicados
            var existe = _contenedorTrabajo.EmpresaRecolectora
                .GetFirstOrDefault(e => e.Nombre
                .ToLower() == empresaRecolectora.Nombre.ToLower());

            if (existe != null)
            {
                return Conflict(new { mensaje = "Ya existe una empresa con ese nombre." });
            }

            // Guardar la nueva empresa recolectora
            _contenedorTrabajo.EmpresaRecolectora.Add(empresaRecolectora);
            _contenedorTrabajo.Save();
            return CreatedAtAction(nameof(GetById), new { id = empresaRecolectora.IDEmpresa }, empresaRecolectora);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Update(int id, [FromBody] EmpresaRecolectora empresaRecolectora)
        {
            var empresaDesdeDb = _contenedorTrabajo.EmpresaRecolectora.GetFirstOrDefault(e => e.IDEmpresa == id);
            if (empresaDesdeDb == null || !ModelState.IsValid)
            {
                return NotFound( new { mensaje = $"No se encontro la empresa con el ID {id}" });
            }
            empresaDesdeDb.Nombre = empresaRecolectora.Nombre;
            empresaDesdeDb.TipoResiduo = empresaRecolectora.TipoResiduo;
            _contenedorTrabajo.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
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