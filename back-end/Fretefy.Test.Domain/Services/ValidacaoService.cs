using Fretefy.Test.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Services
{
    public class ValidacaoService : IValidacaoService
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
