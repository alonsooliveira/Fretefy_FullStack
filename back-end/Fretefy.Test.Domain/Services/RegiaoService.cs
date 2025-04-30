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

            regiao.Nome = regiaoDTO.Nome;
            regiao.RegiaoCidades = regiaoDTO.Cidades.ToList().Select(cidadeId => new RegiaoCidade
            {
                Cidade = new Cidade
                {
                    Id = cidadeId
                }
            }).ToList();

            await _regiaoRepository.Atualizar(regiao);
        }

        public async Task<IEnumerable<ListarRegiaoDTO>> ListarRegiao()
        {

            var regioes = _regiaoRepository.Listar().Select(p => new ListarRegiaoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Ativo = p.Ativo,
                Cidades = null
            }).ToList();

            return regioes;
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

            if (regiao.VerificarCidadeDuplicada())
            {
                throw new Exception("Cidade Duplicada na região");
            }

            await _regiaoRepository.Salvar(regiao);
        }
    }
}
