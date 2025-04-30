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
        private DbSet<Regiao> _dbSet;
        private readonly DbContext _context;

        public RegiaoRepository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<Regiao>();
        }
        public async Task Atualizar(Regiao regiao)
        {
            _context.Set<Regiao>().Update(regiao);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Regiao> Listar()
        {
            return _context.Set<Regiao>().AsQueryable();
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
