using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly DbContext _context;

        public RegiaoRepository(DbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task Atualizar(Regiao regiao)
        {
            _context.Set<Regiao>().Update(regiao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverCidades(Regiao regiao)
        {
            _context.Set<RegiaoCidade>().RemoveRange(regiao.RegiaoCidades);
        }

        public async Task<List<Regiao>> Listar()
        {
            return await _context.Set<Regiao>()
                .Include(p => p.RegiaoCidades)
                .ThenInclude(x => x.Cidade)
                .ToListAsync();
        }

        public async Task<Regiao> ListarPorId(Guid id)
        {
            return await _context.Set<Regiao>()
                .Include(p => p.RegiaoCidades)
                .ThenInclude(x => x.Cidade)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Salvar(Regiao regiao)
        {
            _context.Set<Regiao>().Add(regiao);
            await _context.SaveChangesAsync();
        }
    }
}
