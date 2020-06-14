using System;
using SD.Domain.Params;
using SD.Domain.Enums;

using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Service
{
    public interface ILancamentoService
    {
        Task<Operacao> EfetuaLancamento(ContaCorrente contaOrigem, ContaCorrente contaDestino, OperacaoTipo tipoOperacao, decimal valor);
    }
}
