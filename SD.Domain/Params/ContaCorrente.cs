using SD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Domain.Params
{
    public class ContaCorrente
    {
        public int Banco { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public int Digito { get; set; }
        public decimal Saldo { get; set; }
    }
}
