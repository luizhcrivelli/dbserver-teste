using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using Xunit;
using SD.Test.Integration.Fixtures;
using System.Net.Http;

namespace SD.Test.Integration.Scenarios
{
    public class ValuesTest
    {
        private readonly TestContext _testContext;

        public ValuesTest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task ValidarGetInexistente()
        {
            var response = await _testContext.Client.GetAsync("/Conta/EfetuarOperacao");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ValidarLancamentoPostVazio()
        {
            StringContent content = new StringContent("{}");
            var response = await _testContext.Client.PostAsync("/Conta/EfetuarOperacao", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ValidarLancamentoPost()
        {
            string body = "{ \"contaOrigem\": { \"banco\": 353, \"agencia\": 1020, \"conta\": 123456, \"digito\": 4, \"saldo\": 1000 }, \"contaDestino\": { \"banco\": 353, \"agencia\": 1020, \"conta\": 789012, \"digito\": 2, \"saldo\": 5 }, \"tipoOpercao\": 1, \"valor\": 250}";

            var content = new StringContent(body);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _testContext.Client.PostAsync("/Conta/EfetuarOperacao", content);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
