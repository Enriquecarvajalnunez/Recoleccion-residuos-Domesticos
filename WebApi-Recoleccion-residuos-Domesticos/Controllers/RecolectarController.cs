using Microsoft.AspNetCore.Mvc;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using System;
using WebApi_Recoleccion_residuos_Domesticos.Helpers;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace WebApi_Recoleccion_residuos_Domesticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecolectarController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ILogger<RecolectarController> _logger; // Inyectamos el logger para registrar eventos.
        public RecolectarController(IContenedorTrabajo contenedorTrabajo,
                                    ILogger<RecolectarController> logger) // Inyectamos el logger en el constructor.
        {
            _contenedorTrabajo = contenedorTrabajo;
            _logger = logger; // Asignamos el logger a la variable de instancia.
        }


        // Obtenemos todas las solicitudes de recoleccion.
        [HttpGet("solicitudes-de-recoleccion")]
        public IActionResult GetAllSolicitudRecoleccion()
        {
            // Filtramos las solicitudes de recolección por el estado Programada.
            var solicitudes = _contenedorTrabajo.Recolectar.GetAll()
                .Where(r => r.Estado == EstadoRecolectarEnum.Programada);
            return Ok(solicitudes);
        }


        // Obtenemos todas las recolecciones.
        [HttpGet("recolecciones")]
        public IActionResult GetAllRecoleccion()
        {
            // Filtramos las recolecciones por el estado Completada.
            var recolecciones = _contenedorTrabajo.Recolectar.GetAll()
                .Where(r => r.Estado == EstadoRecolectarEnum.Completada);
            return Ok(recolecciones);
        }


        // Obtenemos una recolección por su ID en proceso.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var recoleccion = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id && r.Estado == EstadoRecolectarEnum.Programada);
            if (recoleccion == null)
            {
                return NotFound(new { mensaje = $"No se encontro la recoleccion con ID {id}" });
            }
            return Ok(recoleccion);
        }


        // DTO que define los datos minimos para crear la solicitud de recolección.
        public class SolicitudRecoleccionDTO
        {
            public int IDUsuario { get; set; }
            // Aca recibimos el tipo de residuo en forma de string.
            public string? TipoResiduo { get; set; }
        }
        // Creamos la solicitud de recolección.
        // Solo requerimos el ID del usuario y el tipo de residuo.
        // En la logica de negocio nos encargamos de asignar la empresa y demas informacion necesaria.
        [HttpPost("solicitud-recoleccion")]
        public IActionResult CrearSolicitudRecoleccion([FromBody] SolicitudRecoleccionDTO solicitud)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validamos que el IDUsuario exista.
            var usuarioExiste = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.IDUsuario == solicitud.IDUsuario);
            if (usuarioExiste == null)
            {
                return BadRequest(new { mensaje = $"El usuario no se ha registrado" });
            }

            var tipoResiduo = solicitud.TipoResiduo?.ToLowerInvariant();
            if (tipoResiduo != "inorganico reciclable" && tipoResiduo != "peligroso") // Validamos que el tipo de residuo sea inorganico reciclable o peligroso.
            {
                return BadRequest(new
                {
                    mensaje = "Solo se permite solicitar recoleccion de residuos inorganicos reciclables o peligrosos."
                });
            }

            var allResiduos = _contenedorTrabajo.Residuo.GetAll().AsEnumerable();
            var residuo = allResiduos.FirstOrDefault(r =>
                string.Equals(
                    StringHelper.RemoveDiacritics(r.TipoResiduo.ToString()),
                    StringHelper.RemoveDiacritics(solicitud.TipoResiduo ?? string.Empty),
                    StringComparison.Ordinal));

            // Revisamos que el residuo exista.
            if (residuo == null)
                return NotFound(new { mensaje = $"El tipo de residuo '{solicitud.TipoResiduo}' no existe" });

            // Buscamos el tipo de residuo.
            // Con el _contenedorTrabajo.Residuo.GetAlll() devolvemos todos los tipos de residuos.
            // Realizamos una comparacion ignorando mayusculas y minusculas.
            var existingSolicitud = _contenedorTrabajo.Recolectar.GetAll()
                .AsEnumerable()
                .FirstOrDefault(r =>
                    r.IDUsuario == solicitud.IDUsuario && // Verificamos que el IDUsuario coincida.
                    r.IDResiduo == residuo.IDResiduo && // Verificamos que el IDResiduo coincida.
                    r.Estado == EstadoRecolectarEnum.Programada); // Verificamos que el estado sea Programada.

            if (existingSolicitud != null)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una solicitud de recoleccion en estado programado para este tipo de residuo. " + 
                                "Si necesita corregir la solicitud, por favor eliminela antes de crear una nueva."
                });
            }

            // Normalizamos el valor que viene en la solicitud.
            var normalizedSolicitud = StringHelper.RemoveDiacritics(solicitud.TipoResiduo ?? string.Empty);
            _logger.LogInformation("Residuo en Solicitud (normalizado): '{valorSolicitud}'", normalizedSolicitud);

            // Creamos la solicitud de recoleccion.
            Recolectar nuevaSolicitud = new Recolectar
            {
                IDUsuario = solicitud.IDUsuario,
                IDResiduo = residuo.IDResiduo,
                FechaSolicitudRecoleccion = DateTime.Now, // Asignamos la fecha actual.
                Estado = EstadoRecolectarEnum.Programada // Estado inicial.
            };
            // Guardamos la nueva solicitud en la base de datos.
            _contenedorTrabajo.Recolectar.Add(nuevaSolicitud);
            _contenedorTrabajo.Save();

            _logger.LogInformation("Solicitud creada: IDUsuario={IDUsuario}, IDResiduo={IDResiduo}", solicitud.IDUsuario, residuo.IDResiduo);

            return Ok(new { mensaje = "Solicitud de recoleccion creada correctamente.", solicitud = nuevaSolicitud});
        }


        // DTO que define los datos minimos para crear la recolección.
        public class RecoleccionDTO
        {
            // La empresa debe agregar el ID de la solicitud de recolección.
            public int IDSolicitudRecoleccion { get; set; }
            // La empresa debe agregar el ID que ella tiene asignado.
            public int IDEmpresa { get; set; }
            // La empresa reporta el peso de la recolección.
            [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor a 0")]
            public decimal PesoKg { get; set; }
        }
        // Creamos la recolección.
        // La empresa de la recoleccion es la encargada de gestionar estos datos.
        [HttpPost("recoleccion")]
        public IActionResult CreateRecoleccion([FromBody] RecoleccionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscamos la solicitud existente para la recolección de acuerdo al ID de la solicitud.
            var solicitudRecoleccion = _contenedorTrabajo.Recolectar
                .GetFirstOrDefault(r => r.IDRecoleccion == dto.IDSolicitudRecoleccion);

            if (solicitudRecoleccion == null)
                return NotFound(new { mensaje = $"No se encontro la solicitud de recoleccion con el ID {dto.IDSolicitudRecoleccion}" });

            // Validamos el estado de la solicitud.
            if (solicitudRecoleccion.Estado != EstadoRecolectarEnum.Programada)
                return BadRequest(new { mensaje = "Solo se pueden actualizar solicitudes en estado programada" });
            
            // Actualizamos el registro con la informacion de la recolección.
            // Asiganamos el ID de la empresa que realiza la recolección, el peso y el estado.
            solicitudRecoleccion.IDEmpresa = dto.IDEmpresa;
            solicitudRecoleccion.PesoKg = dto.PesoKg;
            solicitudRecoleccion.FechaRecoleccion = DateTime.Now; // Asignamos la fecha actual.
            solicitudRecoleccion.Estado = EstadoRecolectarEnum.Completada; // Cambiamos el estado a Completada.

            // Guardamos en la base de datos.
            _contenedorTrabajo.Save();

            return Ok(new { mensaje = "Recoleccion actualizada correctamente.", recoleccion = solicitudRecoleccion });
        }

        
        // Eliminamos la solicitud de recolección.
        [HttpDelete("{id}/solicitud-recoleccion")]
        public IActionResult DeleteSolicitudRecoleccion(int id)
        {
            var recolectarDesdeDb = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
            if (recolectarDesdeDb == null)
            {
                return NotFound(new { mensaje = $"No se encontro la solicitud de recoleccion con el ID {id}" });
            }
            // Validamos el estado de la solicitud.
            if (recolectarDesdeDb.Estado != EstadoRecolectarEnum.Programada)
            {
                return BadRequest(new { mensaje = $"Solo se pueden eliminar solicitudes en estado programada" });
            }

            _contenedorTrabajo.Recolectar.Remove(recolectarDesdeDb);
            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "Solicitud de recoleccion eliminada correctamente" });
        }


        // Eliminamos la recolección.
        [HttpDelete("{id}/recoleccion")]
        public IActionResult DeleteRecoleccion(int id)
        {
            var recolectarDesdeDb = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
            if (recolectarDesdeDb == null)
            {
                return NotFound(new { mensaje = $"No se encontro la recoleccion con el ID {id}" });
            }
            // Validamos que solo se puedan eliminar recolecciones completas.
            if (recolectarDesdeDb.Estado != EstadoRecolectarEnum.Completada)
            {
                return BadRequest(new { mensaje = "Solo se pueden eliminar recolecciones completadas" });
            }

            _contenedorTrabajo.Recolectar.Remove(recolectarDesdeDb);
            _contenedorTrabajo.Save();
            return Ok(new { mensaje = "Recoleccion eliminada correctamente" });
        }
    }
}

// Dejamos como ejemplo como se podria implementar un PUT en la solictiud de recolección.
// Creamos un DTO para actualizar el tipo de residuo.
//public class UpdateTipoResiduoDTO
//{
//    public int IDResiduo { get; set; }
//}
// Actualizamos la solicitud de recolección en el caso de que el usuario se equivoque en poner el tipo de residuo.
//[HttpPut("{id}/Solicitud-recolección")]
//public IActionResult Update(int id, [FromBody] UpdateTipoResiduoDTO dto)
//{
//    var recolectarDesdeDb = _contenedorTrabajo.Recolectar.GetFirstOrDefault(r => r.IDRecoleccion == id);
//    if (recolectarDesdeDb == null)
//    {
//        return NotFound(new { mensaje = $"No se encontro la solicitud de recoleccion con el ID {id}" });
//    }
// Verificamos si la solicitud de recolección se pueda modificar en funcion de su estado.
//    if (recolectarDesdeDb.Estado != EstadoRecolectarEnum.Programada)
//    {
//        return BadRequest(new { mensaje = $"Solo se puede modificar una solicitud en estado programada." });
//    }
// Acutalizamos unicamente el tipo de residuo.
//    recolectarDesdeDb.IDResiduo = dto.IDResiduo;
//    
//    _contenedorTrabajo.Save();
//    return Ok(new { mensaje = "Solicitud de recoleccion actualizada correctamente" });
//}

// Para depuracion: logueamos los valores normalizados.
//foreach (var r in allResiduos)
//{
//    var valorBD = StringHelper.RemoveDiacritics(r.TipoResiduo.ToString());
//    _logger.LogInformation("Residuo en DB (normalizado): '{valorBD}'", valorBD);
//}

//if (residuo == null)
//    return NotFound(new { mensaje = $"El tipo de residuo '{solicitud.TipoResiduo}' no existe" });

// Debug para verificar los valores normalizados.
// var valorDB = StringHelper.RemoveDiacritics(residuo.TipoResiduo.ToString());
// Console.WriteLine($"Valor DB: {valorDB} - Valor Solicitud: {normalizedSolicitud}");