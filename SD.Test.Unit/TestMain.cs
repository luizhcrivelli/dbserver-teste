using AutoMapper;
using SD.Domain.Entities;
using SD.Domain.Enums;
using SD.Domain.Interfaces.Repository;
using SD.Domain.Interfaces.Service;
using Params = SD.Domain.Params;
using SD.Domain.Services;
using SD.Infra;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SD.Test.Unit
{
    public class MainTest
    {
        private readonly IContaService _contaService;
        private readonly ILancamentoService _lancamentoService;
        private readonly ILancamentoRepository _lancamentoRepository;

        private Params.ContaCorrente _contaCorrenteOrigem;
        private Params.ContaCorrente _contaCorrenteDestino;

        public MainTest()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ContaCorrente, Params.ContaCorrente>();
                    cfg.CreateMap<Operacao, Params.Operacao>();
                });
            }
            catch (InvalidOperationException)
            {
                //Mapper está criado. ok. 
            }

            _lancamentoRepository = new LancamentoRepository();
            _contaService = new ContaService();
            _lancamentoService = new LancamentoService(_contaService, _lancamentoRepository);

        }

        //Neste teste é esperado que a origem recebe dinheiro do destino
        [Fact]
        public async Task TestaOperacaoCredito()
        {

            _contaCorrenteOrigem = GerarConta(353, 1000, 2002, 3, 1000);
            _contaCorrenteDestino = GerarConta(353, 4000, 5005, 6, 1000);

            var contaOrigem = await _contaService.ValidarConta(_contaCorrenteOrigem);
            var contaDestino = await _contaService.ValidarConta(_contaCorrenteDestino);
            var resultOperacao = await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, OperacaoTipo.Credito, 500);

            Assert.Equal(1500, resultOperacao.ContaOrigem.Saldo);

            Assert.Equal(500, resultOperacao.ContaDestino.Saldo);
        }


        //Neste teste é esperado que saia dinheiro da origem e vá para o destino
        [Fact]
        public async Task TestaOperacaoDebito()
        {
            _contaCorrenteOrigem = GerarConta(353, 1000, 2002, 3, 1000);
            _contaCorrenteDestino = GerarConta(353, 4000, 5005, 6, 1000);

            var contaOrigem = await _contaService.ValidarConta(_contaCorrenteOrigem);
            var contaDestino = await _contaService.ValidarConta(_contaCorrenteDestino);
            var resultOperacao = await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, OperacaoTipo.Debito, 500);

            Assert.Equal(500, resultOperacao.ContaOrigem.Saldo);
            Assert.Equal(1500, resultOperacao.ContaDestino.Saldo);
        }

        //Neste teste é esperado que a origem não tenha saldo suficiente para a operaçao, portanto a operação não é feita
        [Fact]
        public async Task TestaOperacaoDebitoSemSaldoSuficiente()
        {

            _contaCorrenteOrigem = GerarConta(353, 1000, 2002, 3, 1000);
            _contaCorrenteDestino = GerarConta(353, 4000, 5005, 6, 1000);

            var contaOrigem = await _contaService.ValidarConta(_contaCorrenteOrigem);
            var contaDestino = await _contaService.ValidarConta(_contaCorrenteDestino);
            try
            {
                var resultOperacao = await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, OperacaoTipo.Credito, 5000);
            }
            catch ( Exception ex)
            {
                Assert.Equal("Conta de origem não tem saldo suficiente para a transação. Operação não realizada", ex.Message);
            }
        }

        //Neste teste é verificado se o valor a ser creditado está negativo
        [Fact]
        public async Task TestaOperacaoCreditoNegativo()
        {

            _contaCorrenteOrigem = GerarConta(353, 1000, 2002, 3, 1000);
            _contaCorrenteDestino = GerarConta(353, 4000, 5005, 6, 1000);

            var contaOrigem = await _contaService.ValidarConta(_contaCorrenteOrigem);
            var contaDestino = await _contaService.ValidarConta(_contaCorrenteDestino);
            try
            {
                var resultOperacao = await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, OperacaoTipo.Credito, -500);
            }
            catch (Exception ex)
            {
                Assert.Equal("Não é permitido creditar valores negativos", ex.Message);
            }
        }


        
        // Neste teste é verificado se o valor a ser debitado está negativo
        [Fact]
        public async Task TestaOperacaoDebitoNegativo()
        {

            _contaCorrenteOrigem = GerarConta(353, 1000, 2002, 3, 1000);
            _contaCorrenteDestino = GerarConta(353, 4000, 5005, 6, 1000);

            var contaOrigem = await _contaService.ValidarConta(_contaCorrenteOrigem);
            var contaDestino = await _contaService.ValidarConta(_contaCorrenteDestino);
            try
            {
                var resultOperacao = await _lancamentoService.EfetuaLancamento(contaOrigem, contaDestino, OperacaoTipo.Debito, -500);
            }
            catch (Exception ex)
            {
                Assert.Equal("Não é permitido debitar valores negativos", ex.Message);
            }
        }

        private Params.ContaCorrente GerarConta(int banco, int agencia, int conta, int digito, decimal saldo)
        {
            return new Params.ContaCorrente
            {
                Banco = banco,
                Agencia = agencia,
                Conta = conta,
                Digito = digito,
                Saldo = saldo
            };
        }

    }
}
