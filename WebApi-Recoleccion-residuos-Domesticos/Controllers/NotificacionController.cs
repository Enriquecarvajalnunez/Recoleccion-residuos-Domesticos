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
        public IActionResult GetAll()
        {
            var notificaciones = _contenedorTrabajo.Notificacion.GetAll();
            return Ok(notificaciones);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Notificacion.Add(notificacion);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Notificación creada correctamente" });
            }
            return BadRequest(new { mensaje = "Error al crear la notificación" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Notificacion.Update(notificacion);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Notificación actualizada correctamente" });
            }
            return BadRequest(new { mensaje = "Error al actualizar la notificación" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var notificacion = _contenedorTrabajo.Notificacion.GetFirstOrDefault(n => n.Idnotificacion == id);
            if (notificacion != null)
            {
                _contenedorTrabajo.Notificacion.Remove(notificacion);
                _contenedorTrabajo.Save();
                return Ok(new { mensaje = "Notificación eliminada correctamente" });
            }
            return NotFound(new { mensaje = "Notificación no encontrada" });
        }
    }
}