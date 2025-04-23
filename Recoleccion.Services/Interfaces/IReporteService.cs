using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Recoleccion.Services.Interfaces
{
    public interface IReporteService
    {        
        string GenerarReporteEmpresasEnBase64();
        string GenerarReporteEmpresaPorIdEnBase64(int id);
    }
}
