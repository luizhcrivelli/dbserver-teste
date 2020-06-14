using System;
using SD.Domain.Enums;
using SD.Domain.Entities.Base;


namespace SD.Domain.Entities
{
    public class Operacao : EntityBase
    {
        public ContaCorrente ContaOrigem { get; set; }

        public ContaCorrente ContaDestino { get; set; }

        public OperacaoTipo Tipo { get; set; }

        public decimal ValorTransacao { get; set; }

        public DateTime DataRegistroOperacao { get; set; }
    }
}
