using System;
using System.Collections.Generic;
using System.Text;

namespace Fretefy.Test.Domain.DTOs
{
    public class ListarRegiaoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ListarCidadeDTO> Cidades { get; set; }

    }

    public class ListarCidadeDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
