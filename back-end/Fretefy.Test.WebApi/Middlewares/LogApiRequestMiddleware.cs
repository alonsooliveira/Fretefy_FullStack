using Fretefy.Test.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Middlewares
{
    public class LogApiRequestMiddleware : IMiddleware
    {
        private readonly ILogger<LogApiErroMiddleware> _logger;

        public LogApiRequestMiddleware(ILogger<LogApiErroMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            //Request
            var requisicao = new LogRequestDTO
            {
                Data = DateTime.Now,
                Metodo = context.Request.Method,
                Caminho = context.Request.Path.Value
            };

            Stream originalBody = context.Response.Body;

            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Position = 0;
            requisicao.Status = context.Response.StatusCode;
            requisicao.Resposta = new StreamReader(memStream).ReadToEnd();

            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);

            context.Response.Body = originalBody;

            //Ideal gravar no mongoDB
            _logger.LogInformation($"{JsonConvert.SerializeObject(requisicao)}");

        }
    }
}
