using System.Net;
using System.Text.Json;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;

namespace PruebaTecnica.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // El repositorio se inyecta directamente aquí
        public async Task InvokeAsync(HttpContext context, IBitacoraRepository bitacoraRepository)
        {
            try
            {
                // Permite que la petición continúe su camino normal hacia los controladores
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si CUALQUIER error no controlado ocurre en la API, cae aquí automáticamente
                await HandleExceptionAsync(context, ex, bitacoraRepository);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IBitacoraRepository bitacoraRepository)
        {
            // 1. Guardamos el error silenciosamente en la base de datos
            var errorLog = new BitacoraError
            {
                Fecha = DateTime.Now,
                TipoEvento = "Excepción Global",
                Mensaje = exception.Message,
                DetallesExcepcion = exception.StackTrace // El StackTrace nos dice en qué línea exacta falló el código
            };

            try
            {
                await bitacoraRepository.RegistrarAsync(errorLog);
            }
            catch
            {
                // Si la base de datos está caída, evitamos que el middleware en sí mismo arroje otra excepción
            }

            // 2. Formateamos una respuesta amigable para el frontend / cliente
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ha ocurrido un error interno en el servidor. El evento ha sido registrado en la bitácora."
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}