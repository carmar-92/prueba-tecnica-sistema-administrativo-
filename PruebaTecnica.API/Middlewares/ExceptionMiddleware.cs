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
        public async Task InvokeAsync(HttpContext context, IBitacoraRepository bitacoraRepository)
        {
            try
            {                
                await _next(context);
            }
            catch (Exception ex)
            {                
                await HandleExceptionAsync(context, ex, bitacoraRepository);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IBitacoraRepository bitacoraRepository)
        {            
            var errorLog = new BitacoraError
            {
                Fecha = DateTime.Now,
                TipoEvento = "Excepción Global",
                Mensaje = exception.Message,
                DetallesExcepcion = exception.StackTrace 
            };

            try
            {
                await bitacoraRepository.RegistrarAsync(errorLog);
            }
            catch
            {
                
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ha ocurrido un error interno en el servidor."
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}