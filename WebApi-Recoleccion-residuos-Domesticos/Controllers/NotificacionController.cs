using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

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
        public IActionResult GetByUsuario(int idUsuario)
        {
            var notificaciones = _contenedorTrabajo.Notificacion.GetAll()
                                        .Where(n => n.IDUsuario == idUsuario)
                                        .OrderByDescending(n => n.FechaEnvio);
            return Ok(notificaciones);
        }    
    }
}