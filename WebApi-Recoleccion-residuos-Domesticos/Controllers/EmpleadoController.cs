using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApi_Recoleccion_residuos_Domesticos.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly RecoleccionResiduosContext dbContext;

        public EmpleadoController(RecoleccionResiduosContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await dbContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK,listaEmpleado);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Empleado objeto)
        {
            await dbContext.Empleados.AddAsync(objeto);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empleado agregado correctamente"});
        }


        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            dbContext.Empleados.Update(objeto);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empleado Editado correctamente" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            dbContext.Empleados.Remove(empleado);
            
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empleado Eliminado correctamente" });
        }


    }
}
