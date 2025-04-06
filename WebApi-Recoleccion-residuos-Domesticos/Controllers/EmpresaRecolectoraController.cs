using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaRecolectoraController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public EmpresaRecolectoraController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;            
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult GetAll()
        {
            var ListaEmpresaRecolectoras = _contenedorTrabajo.EmpresaRecolectora.GetAll();
            return StatusCode(StatusCodes.Status200OK, ListaEmpresaRecolectoras);
        }

        [HttpPost]
        [Route("Nuevo")]
        public IActionResult Create([FromBody] EmpresaRecolectora EmpRecolect)
        {
            if (EmpRecolect == null) 
            {
                return BadRequest("Datos inválidos");
            }
         

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.EmpresaRecolectora.Add(EmpRecolect);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empresa Recolectora Creada correctamente" });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(EmpresaRecolectora EmpRecolect)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.EmpresaRecolectora.Update(EmpRecolect);
                _contenedorTrabajo.Save();
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empresa Editado" });
        }


        [HttpPost]
        [Route("Eliminar")]
        public IActionResult Delete(int id)
        {
            var objFromBD = _contenedorTrabajo.EmpresaRecolectora.Get(id);
            if (objFromBD == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _contenedorTrabajo.EmpresaRecolectora.Remove(objFromBD);
            _contenedorTrabajo.Save();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empresa eliminada" });
        }
    }
}
