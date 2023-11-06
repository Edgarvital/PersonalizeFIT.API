using System.ComponentModel.DataAnnotations;
namespace PersonalizeFIT.ExerciseAPI.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                var statusCode = GetStatusCode(e);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                switch (statusCode)
                {
                    case 422:
                        await context.Response.WriteAsJsonAsync(new
                        {
                            title = "Erro de Validação",
                            status = statusCode,
                            detail = e.Message
                        });
                        return;
                }

                var response = context.Response;

                var value = new
                {
                    title = "Ocorreu um erro na operação",
                    status = statusCode,
                    detail = e.Message,
                    errors = new List<string>()
                };

                await response.WriteAsJsonAsync(value);
            }
        }

        private static int GetStatusCode(Exception exception)
        {
            var result = exception switch
            {
                InvalidOperationException => 400,
                ArgumentNullException => 400,
                ValidationException => 422,
                _ => 500
            };

            return result;
        }
    }
}