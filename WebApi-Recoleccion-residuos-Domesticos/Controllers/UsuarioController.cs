using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Microsoft.AspNetCore.Cors;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PermitirTodo")]
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
        [Route("Nuevo")]
        public IActionResult CrearUsuario([FromBody]Usuario usuario)
        {

            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre) || usuario.IDLocalidad == 0)
            {
                return BadRequest("Datos inválidos, verifica que todos los campos obligatorios estén completos");
            }

            var localidadValida = _contenedorTrabajo.Localidad.GetFirstOrDefault(l => l.IDLocalidad == usuario.IDLocalidad);
            if (localidadValida == null)
            {
                return BadRequest("La localidad seleccionada no es válida");
            }

            // Agregamos el Console.WriteLine para depuraracion
            Console.WriteLine($"Usuario a guardar: Nombre={usuario.Nombre}, Email={usuario.Email}, Localidad={usuario.IDLocalidad}, Rol={usuario.Rol}");

            try
            {
                _contenedorTrabajo.Usuario.Add(usuario);
                _contenedorTrabajo.Save();
                if (usuario.Localidad != null)
                {
                    usuario.Localidad.Usuarios = null;// Asegurarte de no incluir la lista de Usuarios. - Evitar la referencia circular
                }
                return CreatedAtAction(nameof(GetById), new { id = usuario.IDUsuario },
                    new { mensaje = "El usuario se agrego de forma correcta.", usuario});
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