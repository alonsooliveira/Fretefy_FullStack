using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IValidacaoService _validacaoService;
        public BaseController(IValidacaoService validacaoService)
        {
            _validacaoService = validacaoService;
        }

        public async Task<IActionResult> Response(object obj) 
        {
            if(_validacaoService.Mensagens().Any())
            {
                return BadRequest(new
                {
                    validacoes = _validacaoService.Mensagens().Select(msg => msg)
                });
            }

            return Ok(obj);
        }
    }
}
