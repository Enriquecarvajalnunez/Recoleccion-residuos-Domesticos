using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public NotificacionController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;            
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaLocalidades = _contenedorTrabajo.Localidad.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaLocalidades);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] Notificacion notifica)
        {
            if (notifica == null) 
            {
                return BadRequest("Datos inválidos");
            }      

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Notificacion.Add(notifica);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Notificacion Creada correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(Notificacion notifica)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Notificacion.Update(notifica);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Notificacion Editada" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.Notificacion.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.Notificacion.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Notificacion eliminada" });
        }
    }
}
