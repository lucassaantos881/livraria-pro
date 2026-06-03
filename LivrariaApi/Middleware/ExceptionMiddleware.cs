using System.Net;
using System.Text.Json;

namespace LivrariaApi.Middleware
{
    public class ExceptionMiddleware
    {
        // Representa o próximo passo da requisição, funcionando como uma fila, cada um passa para o próximo
        //next é o que vem depois de mim na fila
        private readonly RequestDelegate _next;

        //Serviço de log do ASP.NET, registra mensagens no console e em arquivos
        private readonly ILogger<ExceptionMiddleware> _logger;


        //Recebe o próximo passo e o serviço de log por meio da injeção de dependência
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //Método assincrono, não bloqueia o servidor enquanto espera a resposta, liberando para atender outras requisições
        //O HttpContext representa toda a informação da requisição e resposta...
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //passa a requisição para o próximo passo da fila, caso estiver tudo certo, resposta normal!
                //o await espera até que o próximo passo seja encerrado
                await _next(context);
            }
            catch(ArgumentException ex){ 

                //LogWarning registra uma mensagem de aviso, indicando o tipo de erro e sua respectiva mensagem
               _logger.LogWarning("Erro de validação: {Message}", ex.Message);
                await EscreverResposta(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Erro de validação: {Message}", ex.Message);
                await EscreverResposta(context, HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }



        private static async Task EscreverResposta(HttpContext context, HttpStatusCode status, string mensagem)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var resposta = new { error = mensagem };
            await context.Response.WriteAsync(JsonSerializer.Serialize(resposta));

        }
    }
}
