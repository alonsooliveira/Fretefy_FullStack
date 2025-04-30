using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ListarRegiaoDTO>> GetAsync()
        {
            return await _regiaoService.ListarRegiao();
        }

        [HttpGet("{id}")]
        public async Task<ListarRegiaoDTO> Get(Guid id)
        {
            return await _regiaoService.ListarRegiaoPorId(id);
        }

     
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarRegiaoDTO regiao)
        {
            await _regiaoService.Salvar(regiao);
            return Ok();
        }

        [HttpPost]
        [Route("ativar")]
        public async Task<IActionResult> Ativar([FromBody] BaseDTO regiao)
        {
            await _regiaoService.Ativar(regiao.Id);
            return Ok();
        }

        [HttpPost]
        [Route("desativar")]
        public async Task<IActionResult> Desativar([FromBody] BaseDTO regiao)
        {
            await _regiaoService.Desativar(regiao.Id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarRegiaoDTO regiao)
        {
            await _regiaoService.Atualizar(regiao);
            return Ok();
        }
    }
}
