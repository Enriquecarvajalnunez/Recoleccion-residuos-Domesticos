using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

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

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaRecolectar = _contenedorTrabajo.Recolectar.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaRecolectar);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] Recolectar recolectar)
        {
            if (recolectar == null) 
            {
                return BadRequest("Datos inválidos");
            }      

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Recolectar.Add(recolectar);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Recolectar Creado correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(Recolectar recolectar)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Recolectar.Update(recolectar);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Recolectar Editado" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.Recolectar.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.Recolectar.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Recolectar eliminado" });
        }
    }
}
