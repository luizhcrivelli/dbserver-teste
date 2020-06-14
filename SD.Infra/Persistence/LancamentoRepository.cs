using SD.Domain.Entities;
using SD.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace SD.Infra
{
    public class LancamentoRepository : ILancamentoRepository
    {
        public async Task RegistrarLancamento(Operacao operacao)
        {
            //salvar lançamenteo em umn repositório;
        }
    }
}
