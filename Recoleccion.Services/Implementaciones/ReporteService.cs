using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Recoleccion.AccesoDatos.Data;
using Recoleccion.AccesoDatos.Data.Repository;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Services.Interfaces;

namespace Recoleccion.Services.Implementaciones
{
    public class ReporteService : IReporteService
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ReporteService(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        
        //Reporte de Empresas
        public string GenerarReporteEmpresasEnBase64()
        {
            var fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                
                // Título "CLEAN ENVIRONMENT"
                var tituloPrincipal = new Paragraph("CLEAN ENVIRONMENT")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                document.Add(tituloPrincipal);
                document.Add(new Paragraph("\n"));

                // Título de la empresa
                var titulo = new Paragraph("REPORTE DE EMPRESAS RECOLECTORAS")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16)
                    .SetFont(fontBold);
                document.Add(titulo);
                document.Add(new Paragraph("\n"));

                // Definir tabla
                Table tabla = new Table(new float[] { 1, 3, 3 }).UseAllAvailableWidth();

                // Encabezados               
                tabla.AddHeaderCell("Nombre");
                tabla.AddHeaderCell("Tipo de Residuo");

                // Obtener datos
                var empresas = _contenedorTrabajo.EmpresaRecolectora.GetAll();

                foreach (var empresa in empresas)
                {                    
                    tabla.AddCell(empresa.Nombre);
                    tabla.AddCell(empresa.TipoResiduo);
                }

                // Agregar fecha y hora de generación al final
                var fechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var footer = new Paragraph($"Reporte Generado el: {fechaHora}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(10)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                document.Add(new Paragraph("\n"));
                document.Add(footer);

                document.Add(tabla);
                document.Close();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public string GenerarReporteEmpresaPorIdEnBase64(int idEmpresa)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Título "CLEAN ENVIRONMENT"
                var tituloPrincipal = new Paragraph("CLEAN ENVIRONMENT")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                document.Add(tituloPrincipal);
                document.Add(new Paragraph("\n"));

                // Título de la empresa
                var titulo = new Paragraph($"REPORTE DE LA EMPRESA: {idEmpresa}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                document.Add(titulo);
                document.Add(new Paragraph("\n"));

                // Obtener la empresa por ID
                var empresa = _contenedorTrabajo.EmpresaRecolectora.Get(idEmpresa);
                if (empresa == null)
                {
                    // Si no se encuentra la empresa
                    document.Add(new Paragraph("No se encontró la empresa con el ID proporcionado."));
                }
                else
                {
                    // Crear tabla
                    Table tabla = new Table(new float[] { 1, 3, 3 }).UseAllAvailableWidth();                    
                    tabla.AddHeaderCell("Nombre");
                    tabla.AddHeaderCell("Tipo de Residuo");

                    // Agregar datos de la empresa                    
                    tabla.AddCell(empresa.Nombre);
                    tabla.AddCell(empresa.TipoResiduo);

                    document.Add(tabla);
                }

                // Agregar fecha y hora de generación al final
                var fechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var footer = new Paragraph($"Reporte Generado el: {fechaHora}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(10)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                document.Add(new Paragraph("\n"));
                document.Add(footer);


                document.Close();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }


    }
}
