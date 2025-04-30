using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        public Guid Id { get ; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<RegiaoCidade> RegiaoCidades { get; set; }

        public Regiao()
        {

        }

        public Regiao(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
        public bool VerificarCidadeDuplicada()
        {
            return RegiaoCidades.GroupBy(x => x.CidadeId).Any(g => g.Count() > 1);
        }
    }
}
