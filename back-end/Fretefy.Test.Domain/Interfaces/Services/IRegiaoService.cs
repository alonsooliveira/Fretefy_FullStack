using Fretefy.Test.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        Task Salvar(AdicionarRegiaoDTO regiaoDTO);
        Task Atualizar(AtualizarRegiaoDTO regiaoDTO);
        Task<IEnumerable<ListarRegiaoDTO>> ListarRegiao();
        Task<ListarRegiaoDTO> ListarRegiaoPorId(Guid id);
        Task Ativar(Guid id);
        Task Desativar(Guid id);
    }
}
