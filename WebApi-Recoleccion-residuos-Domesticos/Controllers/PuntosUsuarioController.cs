using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

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

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaCanjePuntos = _contenedorTrabajo.PuntosUsuario.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaCanjePuntos);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] PuntosUsuario puntos)
        {
            if (puntos == null) 
            {
                return BadRequest("Datos inválidos");
            }      

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.PuntosUsuario.Add(puntos);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Puntos Creado correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(PuntosUsuario puntos)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.PuntosUsuario.Update(puntos);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Puntos Editados" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.PuntosUsuario.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.PuntosUsuario.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Puntos eliminados" });
        }
    }
}
