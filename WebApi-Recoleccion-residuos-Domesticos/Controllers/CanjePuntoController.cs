using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanjePuntoController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CanjePuntoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;            
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaCanjePuntos = _contenedorTrabajo.CanjePunto.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaCanjePuntos);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] CanjePunto canjePunto)
        {
            if (canjePunto == null) 
            {
                return BadRequest("Datos inválidos");
            }

            // Verificar si el usuario ya existe en la BD usando Unit of Work
            var usuarioExistente = _contenedorTrabajo.Usuario.Get(canjePunto.Idusuario);

            if (usuarioExistente == null)
            {
                return NotFound($"No se encontró un usuario con ID {canjePunto.Idusuario}");
            }

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.CanjePunto.Add(canjePunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "canje Punto Creado correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(CanjePunto canjePunto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.CanjePunto.Update(canjePunto);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "CanjePunto Editado" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.CanjePunto.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.CanjePunto.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "CanjePunto eliminado" });
        }
    }
}
