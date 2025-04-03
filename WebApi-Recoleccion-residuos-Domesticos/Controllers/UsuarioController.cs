using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

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
     
        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Usuario.Add(usuario);
                _contenedorTrabajo.Save();                
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }       
    }
}
