using LabScore.io.Server.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace LabScore.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = exception switch
            {
                EntidadeNaoEncontradaException => HttpStatusCode.NotFound, 
                SimuladoInvalidoException => HttpStatusCode.BadRequest,    
                AlternativaInconsistenteException => HttpStatusCode.UnprocessableEntity, 
                DbUpdateException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError 
            };

            var mensagem = exception.Message;
            if (exception is DbUpdateException dbEx && dbEx.InnerException is not null)
            {
                mensagem = $"{dbEx.Message} | Detalhe: {dbEx.InnerException.Message}";
            }

            // Objeto de resposta detalhado
            var response = new
            {
                codigoHttp = (int)code,
                mensagem,
                sucesso = false,
                dataHora = DateTime.UtcNow
            };

            var result = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}

