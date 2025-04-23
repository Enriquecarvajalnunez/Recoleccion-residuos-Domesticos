using Microsoft.AspNetCore.Mvc;
using Recoleccion.Services.Interfaces;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }
        
        [HttpGet("empresas/pdf")]
        public IActionResult ObtenerReporteEmpresas()
        {
            var base64 = _reporteService.GenerarReporteEmpresasEnBase64();
            return Ok(new
            {
                archivo = base64,
                tipoContenido = "application/pdf",
                nombreArchivo = "reporte_empresas.pdf"
            });
        }

        [HttpGet("empresa/{id}/pdf")]
        public IActionResult ObtenerReporteEmpresaPorId(int id)
        {
            var base64 = _reporteService.GenerarReporteEmpresaPorIdEnBase64(id);
            return Ok(new
            {
                archivo = base64,
                tipoContenido = "application/pdf",
                nombreArchivo = $"reporte_empresa_{id}.pdf"
            });
        }



    }
}
