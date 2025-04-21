using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;

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

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult GetByUsuario(int idUsuario)
        {
            var puntosUsuario = _contenedorTrabajo.PuntosUsuario.GetAll()
                                        .Where(n => n.IDUsuario == idUsuario);
            return Ok(puntosUsuario);
        }
    }
}
