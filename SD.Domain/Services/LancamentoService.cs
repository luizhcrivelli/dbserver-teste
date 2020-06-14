using AutoMapper;
using SD.Domain.Params;
using SD.Domain.Entities;
using SD.Domain.Enums;
using SD.Domain.Interfaces.Repository;
using SD.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace SD.Domain.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly IContaService _contaService;
        private readonly ILancamentoRepository _lancamentoRepository;

        public LancamentoService(IContaService contaService, ILancamentoRepository lancamentoRepository)
        {
            _contaService = contaService;
            _lancamentoRepository = lancamentoRepository;
        }

        public async Task<Params.Operacao> EfetuaLancamento(Params.ContaCorrente contaOrigem, Params.ContaCorrente contaDestino, OperacaoTipo tipoOperacao, decimal valor)
        {
            Params.ContaCorrente origem = new Params.ContaCorrente();
            Params.ContaCorrente destino = new Params.ContaCorrente();

            if (contaOrigem.Agencia == contaDestino.Agencia && contaOrigem.Conta == contaDestino.Conta)
                throw new ArgumentException("Conta de origem e destino são as mesmas. Operação não realizada");

            if (!await _contaService.ExisteSaldoParaTransacao(contaOrigem,valor))
                throw new ArgumentException("Conta de origem não tem saldo suficiente para a transação. Operação não realizada");

            if (tipoOperacao == OperacaoTipo.Credito)
            {
                origem = await _contaService.Credito(contaOrigem, valor);
                destino = await _contaService.Debito(contaDestino, valor);
            }

            if (tipoOperacao == OperacaoTipo.Debito)
            {
                origem = await _contaService.Debito(contaOrigem, valor);
                destino = await _contaService.Credito(contaDestino, valor);
            }

            var operacao = new Entities.Operacao
            {
                ContaOrigem = Mapper.Map<Entities.ContaCorrente>(origem),
                ContaDestino = Mapper.Map<Entities.ContaCorrente>(destino),
                DataRegistroOperacao = DateTime.Now,
                ValorTransacao = valor,
                Tipo = tipoOperacao
            };

            await _lancamentoRepository.RegistrarLancamento(operacao);

            return Mapper.Map<Params.Operacao>(operacao);

        }
    }
}
