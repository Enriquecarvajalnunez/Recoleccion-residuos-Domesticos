using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosUsuarioController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public PuntosUsuarioController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] PuntosUsuario puntosUsuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.PuntosUsuario.Add(puntosUsuario);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro creado correctamente" });
        }

        [HttpPut]
        [Route("Actualizar")]
        public IActionResult Update([FromBody] PuntosUsuario puntosUsuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.PuntosUsuario.Update(puntosUsuario);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro actualizado correctamente" });
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.