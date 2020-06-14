using SD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Repository
{
    public interface ILancamentoRepository
    {
        Task RegistrarLancamento(Operacao operacao);
    }
}
