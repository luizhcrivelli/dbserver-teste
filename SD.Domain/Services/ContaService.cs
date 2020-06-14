using AutoMapper;
using System;
using System.Threading.Tasks;
using SD.Domain.Entities;
using SD.Domain.Interfaces.Service;
using Params = SD.Domain.Params;

namespace SD.Domain.Services
{
    public class ContaService : IContaService
    {
        public ContaService()
        {
        }

        public async Task<Params.ContaCorrente> ValidarConta(Params.ContaCorrente conta)
        {
            // Para este teste inseri algumas validações simples.
            // obviamente elas não refletem a realidade das validações necessárias ref ao banco, agencia, conta e dígito.

            if (conta == null)
                throw new ArgumentException("Conta inválida.");

            if (conta.Banco < 0 || conta.Banco.ToString().Length != 3)
                throw new ArgumentException($"Número do banco ({conta.Banco}) inválido.");

            if (conta.Agencia < 0 || conta.Agencia.ToString().Length != 4)
                throw new ArgumentException($"Número da agência ({conta.Agencia}) inválido.");

            if (conta.Conta < 0)
                throw new ArgumentException($"Número da conta ({conta.Conta}) inválido.");

            if (conta.Digito < 0 || conta.Digito.ToString().Length != 1)
                throw new ArgumentException($"Digíto da conta ({conta.Digito}) inválido.");

            return Mapper.Map<Params.ContaCorrente>(new Entities.ContaCorrente(conta.Banco, conta.Agencia, conta.Conta, conta.Digito, conta.Saldo));

        }

        public async Task<Params.ContaCorrente> Credito(Params.ContaCorrente conta, decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("Não é permitido creditar valores negativos");

            conta.Saldo = conta.Saldo + valor;
            return conta;
        }

        public async Task<Params.ContaCorrente> Debito(Params.ContaCorrente conta, decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("Não é permitido debitar valores negativos");

            conta.Saldo = conta.Saldo - valor;
            return conta;

        }

        public async Task<bool> ExisteSaldoParaTransacao(Params.ContaCorrente conta, decimal valor)
        {
            //Para este teste não trabalharei com limite de saldo negativo pois não é esta a função desejada.
            //só inseri esta verificação para emular uma verificação de outro microserviço específico para isso.
            return (conta.Saldo - valor >= 0); 
        }
    }
}
