using System;
using System.Collections.Generic;
using System.Text;

namespace Fretefy.Test.Domain.DTOs
{
    public class AdicionarRegiaoDTO
    {
        public string Nome { get; set; }
        public IEnumerable<Guid> Cidades { get; set; }
    }
}
