using SD.Domain.Params;
using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Service
{
    public interface IOperacaoService
    {
        Task<Operacao> EfetuarOperacao(Operacao argument);
    }
}
