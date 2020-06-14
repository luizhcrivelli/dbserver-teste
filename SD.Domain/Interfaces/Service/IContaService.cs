using SD.Domain.Params;
using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Service
{
    public interface IContaService
    {
        Task<ContaCorrente> ValidarConta(ContaCorrente conta);
        Task<ContaCorrente> Credito(ContaCorrente conta, decimal valor);
        Task<ContaCorrente> Debito(ContaCorrente conta, decimal valor);
        Task<bool> ExisteSaldoParaTransacao(ContaCorrente conta, decimal valor);
    }
}
