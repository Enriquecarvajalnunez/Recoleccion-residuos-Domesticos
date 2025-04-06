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


        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll() 
        {            
            
            var listaUsuarios =  _contenedorTrabajo.Usuario.GetAll();                        
            return StatusCode(StatusCodes.Status200OK, listaUsuarios);
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


        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Usuario.Update(usuario);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id) 
        { 
            var objFromBD = _contenedorTrabajo.Usuario.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.Usuario.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario eliminado" });
        }
    }
}
