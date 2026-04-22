using Microsoft.AspNetCore.Mvc;
using LicenciasAPI.Data;
using LicenciasAPI.Models;

namespace LicenciasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LicenciasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("solicitar")]
        public async Task<IActionResult> ProcesarSolicitud([FromBody] SolicitudLicencia solicitud)
        {
            //1. Validación básica de edad
            if (solicitud.Edad < 17)
            {
                return BadRequest(new { mensaje = "Edad insuficiente para cualquier grado de licencia." });
            }

            //2. Lógica de negocio para aprobación
            // Calculamos el promedio de las notas
            double promedio = (solicitud.NotaTeorica + solicitud.NotaPractica) / 2;

            // Aplicamos reglas por grado (ejemplo: grados altos requieren mas puntaje)
            double minimoExigido = solicitud.Grado switch
            {
                5 => 85, // Carga pesada exige excelencia
                4 => 80, // Comercial
                _ => 70 // Particular y motos
            };

            solicitud.Aprobado = promedio >= minimoExigido;

            //3. Persistencia en SQL Server
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();

            //4. Respuesta estructurada para n8n
            return Ok(new {
                idTransaccion = solicitud.Id,
                nombreCompleto = $"{solicitud.Nombre} {solicitud.Apellido}",
                resultado = solicitud.Aprobado ? "Aprobada" : "Rechazada",
                gradoAsignado = solicitud.Grado,
                enviarCorreo = true, // Indicamos a n8n que debe enviar un correo
                mensajeParaUsuario = solicitud.Aprobado 
                    ? $"¡Felicidades {solicitud.Nombre}! Tu solicitud de licencia ha sido aprobada. Diríjase a la oficina central en 7 días para retirar su carnet." 
                    : $"Lo sentimos {solicitud.Nombre}, tu solicitud de licencia ha sido rechazada. Por favor, revisa los requisitos y vuelve a intentarlo.",
                emailDestino = solicitud.Email
            });
        }
    }
}
