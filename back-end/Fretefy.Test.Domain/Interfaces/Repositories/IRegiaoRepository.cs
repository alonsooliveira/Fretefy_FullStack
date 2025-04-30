using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        IQueryable<Regiao> Listar();
        Task<Regiao> ListarPorId(Guid id);
        Task Salvar(Regiao regiao);
        Task Atualizar(Regiao regiao);
    }
}
