using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SD.Domain.Interfaces.Service;
using SD.Domain.Params;

namespace SD.Api.Controllers
{
    [Route("Conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IOperacaoService _operacaoService ;

        public ContaController(IOperacaoService contaService)
        {
            _operacaoService = contaService;
        }

        /// <summary>
        /// Função para fazer uma operação de débito (origem) e crédito (destino) nas contas correntes
        /// </summary>
        /// <param name="operacao">objeto contendo as contas e valor de transferência esperado para a operação</param>
        /// <returns></returns>
        /// <response code="200">Operação de transferência efetuada com sucesso</response>
        /// <response code="400">Caso alguma informação estiver errada é retornada a informação</response>
        [HttpPost("EfetuarOperacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EfetuarOperacao([FromBody] Operacao operacao)
        {
            try
            {
                var opr = await _operacaoService.EfetuarOperacao(operacao);
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }

}

/*
{ "contaOrigem": { "banco": 353, "agencia": 1020, "conta": 123456, "digito": 4, "saldo": 1000 }
, "contaDestino": { "banco": 353, "agencia": 1020, "conta": 789012, "digito": 2, "saldo": 5 }
, "tipoOpercao": 1
, "valor": 250 
}
*/
