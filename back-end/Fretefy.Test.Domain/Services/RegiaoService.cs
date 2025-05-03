using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly IValidacaoService _validacaoService;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public async Task Ativar(Guid id)
        {
            var regiao = await _regiaoRepository.ListarPorId(id);

            regiao.Ativar();

            await _regiaoRepository.Atualizar(regiao);

        }

        public async Task Desativar(Guid id)
        {
            var regiao = await _regiaoRepository.ListarPorId(id);

            regiao.Desativar();

            await _regiaoRepository.Atualizar(regiao);

        }

        public async Task Atualizar(AtualizarRegiaoDTO regiaoDTO)
        {
            var regiao = await _regiaoRepository.ListarPorId(regiaoDTO.Id);

            await _regiaoRepository.RemoverCidades(regiao);

            regiao.Nome = regiaoDTO.Nome;
            regiao.RegiaoCidades = regiaoDTO.Cidades.ToList().Select(cidadeId => new RegiaoCidade
            {
                CidadeId = cidadeId,
                RegiaoId = regiaoDTO.Id
            }).ToList();

            await Validar(regiao);

            await _regiaoRepository.Atualizar(regiao);
        }

        public async Task<IEnumerable<ListarRegiaoDTO>> ListarRegiao()
        {
            var regioes = await _regiaoRepository.Listar();

            return regioes.Select(p => new ListarRegiaoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Ativo = p.Ativo,
                Cidades = null
            }).ToList();
        }

        public async Task<ListarRegiaoDTO> ListarRegiaoPorId(Guid id)
        {

            var regiao = await _regiaoRepository.ListarPorId(id);

            return new ListarRegiaoDTO
            {
                Id = regiao.Id,
                Nome = regiao.Nome,
                Cidades = regiao.RegiaoCidades.Select(c => new ListarCidadeDTO
                {
                    Id = c.CidadeId,
                    Nome = c.Cidade.Nome
                })
            };
        }

        public async Task Salvar(AdicionarRegiaoDTO regiaoDTO)
        {
            var regiao = new Regiao(regiaoDTO.Nome)
            {
                RegiaoCidades = regiaoDTO.Cidades.ToList().Select(cidadeId => new RegiaoCidade
                {
                    CidadeId = cidadeId
                }).ToList()
            };

            regiao.Ativar();

            await Validar(regiao);

            await _regiaoRepository.Salvar(regiao);
        }

        private async Task Validar(Regiao regiao)
        {
            if (regiao.VerificarCidadeDuplicada())
            {
                _validacaoService.AddErro("Cidade Duplicada na região");
            }

            var regioes = await _regiaoRepository.Listar();

            if (regiao.VerificarRegiaoComNomeIgual(regioes))
            {
                _validacaoService.AddErro("Região com o mesmo nome já cadastrada");
            }
        }

        public async Task<IEnumerable<ExportarRegiaoDTO>> Exportar()
        {
            var regioes = await _regiaoRepository.Listar();

            return regioes.Select(p => new ExportarRegiaoDTO
            {
                Regiao = p.Nome,
                Ativo = p.Ativo ? "Ativo" : "Inativo",
                Uf = p.RegiaoCidades.Where(x => x.RegiaoId == p.Id).First().Cidade.UF,
                Cidade = p.RegiaoCidades.Where(x => x.RegiaoId == p.Id).First().Cidade.Nome,
            }).ToList();
        }
    }
}
