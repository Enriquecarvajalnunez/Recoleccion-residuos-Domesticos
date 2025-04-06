using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public LocalidadController(IContenedorTrabajo contenedorTrabajo)
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
        public IActionResult Create([FromBody] Localidad localidad)
        {
            if (localidad == null) 
            {
                return BadRequest("Datos inválidos");
            }      

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Localidad.Add(localidad);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Localidad Creada correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Localidad.Update(localidad);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Localidad Editada" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.Localidad.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.Localidad.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Localidad eliminada" });
        }
    }
}
