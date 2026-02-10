using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace TechStore.Middlewares
{
    public class TratamentoExcecoesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TratamentoExcecoesMiddleware> _logger;

        public TratamentoExcecoesMiddleware(
            RequestDelegate next,
            ILogger<TratamentoExcecoesMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro tratado pelo middleware de exceções.");
                await TratarExcecaoAsync(context, ex);
            }
        }

        private static Task TratarExcecaoAsync(HttpContext context, Exception excecao)
        {
            HttpStatusCode statusCode;
            string mensagem;

            switch (excecao)
            {
                case ArgumentException or ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    mensagem = excecao.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    mensagem = excecao.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    mensagem = "Erro interno inesperado.";
                    break;
            }


            var resposta = new
            {
                erro = mensagem
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(resposta)
            );
        }
    }
}
