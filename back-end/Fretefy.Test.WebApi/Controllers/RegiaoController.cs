﻿using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoController : BaseController
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(
            IRegiaoService regiaoService,
            IValidacaoService validacaoService) 
            : base(validacaoService)
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
            return await Response(_regiaoService.Salvar(regiao));
        }

        [HttpPost]
        [Route("ativar")]
        public async Task<IActionResult> Ativar([FromBody] BaseDTO regiao)
        {
            return await Response(_regiaoService.Ativar(regiao.Id));
        }

        [HttpPost]
        [Route("desativar")]
        public async Task<IActionResult> Desativar([FromBody] BaseDTO regiao)
        {
            return await Response(_regiaoService.Desativar(regiao.Id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarRegiaoDTO regiao)
        {
            return await Response(_regiaoService.Atualizar(regiao));
        }

        [HttpGet]
        [Route("exportar")]
        public async Task<FileResult> Exportar()
        {
            var regioes = await _regiaoService.Exportar();

            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                await streamWriter.WriteLineAsync($"Regiao, Situação, UF, Cidade");
                foreach (var p in regioes)
                {
                    await streamWriter.WriteLineAsync($"{p.Regiao}, {p.Ativo}, {p.Uf}, {p.Cidade}");
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }

            return File(memoryStream.ToArray(), "text/csv", $"regioies-{DateTime.Now:s}.csv");
        }
    }
}
