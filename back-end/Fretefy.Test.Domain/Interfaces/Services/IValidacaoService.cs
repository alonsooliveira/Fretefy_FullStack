using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IValidacaoService
    {
        void AddErro(string erro);
        IEnumerable<string> Mensagens();
    }
}
