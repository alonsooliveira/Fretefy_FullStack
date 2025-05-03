using System;
using System.Collections.Generic;
using System.Text;

namespace Fretefy.Test.Domain.DTOs
{
    public class LogRequestDTO
    {
        public DateTime Data { get; set; }
        public string Metodo { get; set; }
        public string Caminho  { get; set; }
        public string Requisicao { get; set; }
        public int Status { get; set; }
        public string Resposta { get; set; }
    }
}
