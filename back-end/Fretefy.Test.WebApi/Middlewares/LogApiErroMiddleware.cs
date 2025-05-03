using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Middlewares
{
    public class LogApiErroMiddleware : IMiddleware
    {
        private readonly ILogger<LogApiErroMiddleware> _logger;

        public LogApiErroMiddleware(ILogger<LogApiErroMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Log do erro (Ideal é gravar no mongoDb)
                var errorCode = string.Format("code-{0}-{1}", new Random().Next(1, 9999), DateTime.Now.ToString("yyyyMMddHHmmss"));
                _logger.LogError($"{JsonConvert.SerializeObject(ex)}");

                context.Response.StatusCode = StatusCodes.Status400BadRequest; 
                await context.Response.WriteAsync($"Ops! Ocorreu um erro no servidor. {errorCode}"); 
            }
        }
    }
}
