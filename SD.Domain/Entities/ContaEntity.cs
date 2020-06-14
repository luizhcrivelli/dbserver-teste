using SD.Domain.Entities.Base;

namespace SD.Domain.Entities
{
    public class ContaCorrente : EntityBase
    {
        public ContaCorrente(int banco, int agencia, int conta, int digito, decimal saldo)
        {
            Banco = banco;
            Agencia = agencia;
            Conta = conta;
            Digito = digito;
            Saldo = saldo;
        }

        public int Banco { get; protected set; }
        public int Agencia { get; protected set; }
        public int Conta { get; protected set; }
        public int Digito { get; protected set; }
        public decimal Saldo { get; protected set; }


    }
}
