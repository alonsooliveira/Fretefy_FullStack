using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    class ValidacaoService : IValidacaoService
    {
        public IList<string> Erros { get; set; }
        public ValidacaoService()
        {
            Erros = new List<string>();
        }
        
        public void AddErro(string erro)
        {
            Erros.Add(erro);
        }

        public IEnumerable<string> Mensagens() => Erros;
       
    }
}
