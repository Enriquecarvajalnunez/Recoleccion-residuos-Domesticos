using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResiduoController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ResiduoController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;            
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaResiduo = _contenedorTrabajo.Residuo.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaResiduo);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] Residuo residuo)
        {
            if (residuo == null) 
            {
                return BadRequest("Datos inválidos");
            }      

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Residuo.Add(residuo);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Residuo Creado correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(Residuo residuo)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Residuo.Update(residuo);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Residuo Editado" });
        }

        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.Residuo.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.Residuo.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Residuo eliminado" });
        }
    }
}
