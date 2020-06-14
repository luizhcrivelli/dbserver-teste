using SD.Domain.Enums;

namespace SD.Domain.Params
{
    public class Operacao
    {
        public ContaCorrente ContaOrigem { get; set; }
        public ContaCorrente ContaDestino { get; set; }
        public OperacaoTipo TipoOpercao { get; set; }
        public decimal Valor { get; set; }
    }
}
