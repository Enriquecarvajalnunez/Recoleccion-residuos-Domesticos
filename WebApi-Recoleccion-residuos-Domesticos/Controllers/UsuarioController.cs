using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
     
        public UsuarioController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _contenedorTrabajo.Usuario.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.IDUsuario == id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"El Usuario con el ID {id} no se encontro"});
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("api/Usuario/Nuevo")]
        public IActionResult CrearUsuario([FromBody]Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Los datos del usuario son invalidos");
            }
            try
            {
                _contenedorTrabajo.Usuario.Add(usuario);
                _contenedorTrabajo.Save();
                return CreatedAtAction(nameof(CrearUsuario), new { id = usuario.IDUsuario}, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Usuario usuario)
        {
            var usuarioDesdeDb = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.IDUsuario == id);
            if (usuarioDesdeDb == null)
            {
                return NotFound(new { mensaje = $"El Usuario con el ID {id} no se encontro" });
            }
            usuarioDesdeDb.Nombre = usuario.Nombre;
            usuarioDesdeDb.Apellidos = usuario.Apellidos;
            usuarioDesdeDb.Telefono = usuario.Telefono;
            usuarioDesdeDb.Email = usuario.Email;
            usuarioDesdeDb.Direccion = usuario.Direccion;
            usuarioDesdeDb.Rol = usuario.Rol;
            usuarioDesdeDb.IDLocalidad = usuario.IDLocalidad;

            _contenedorTrabajo.Save();

            return Ok(new { mensaje = "El Usuario ha sido actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuarioDesdeDb = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.IDUsuario == id);
            if (usuarioDesdeDb == null)
            {
                return NotFound(new { mensaje = $"El Usuario con el ID {id} no se encontro" });
            }
            _contenedorTrabajo.Usuario.Remove(usuarioDesdeDb);
            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "El Usuario ha sido eliminado correctamente" });
        }
    }
}

// GetAll: Con el recuperamos todos los registros de la tabla.
// GetByld: Con el obtenemos un registro especifico por su id.
// Create: Creamos un nuevo registro en la tabla.
// Update: Actualizamos un registro existente en la tabla.
// Delete: Eliminamos un registro especifico de la tabla.