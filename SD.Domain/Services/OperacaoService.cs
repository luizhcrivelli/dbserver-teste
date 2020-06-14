using AutoMapper;
using SD.Domain.Entities;
using SD.Domain.Interfaces.Service;
using Params = SD.Domain.Params;
using System.Threading.Tasks;

namespace SD.Domain.Services
{
    public class OperacaoService : IOperacaoService
    {
        private readonly IContaService _contaService;
        private readonly ILancamentoService _lancamentoService;

        public OperacaoService(IContaService contaService, ILancamentoService lancamentoService)
        {
            _contaService = contaService;
            _lancamentoService = lancamentoService;

            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ContaCorrente, Params.ContaCorrente>();
                    cfg.CreateMap<Operacao, Params.Operacao>();
                });
            }
            catch (System.Exception)
            {
            }

        }

        public async Task<Params.Operacao> EfetuarOperacao(Params.Operacao operacao)
        {
            //Para este exemplo somente é validado as contas recebidas de forma simples e é efetuado o lançamento, conforme solicitado no teste
            var contaOrigem = await _contaService.ValidarConta(operacao.ContaOrigem);
            var contaDestino = await _contaService.ValidarConta(operacao.ContaDestino);

            return await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, operacao.TipoOpercao, operacao.Valor);
        }
    }
}
