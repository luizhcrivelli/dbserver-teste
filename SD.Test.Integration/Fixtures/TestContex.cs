using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SD.Api;
using System.Net.Http;

namespace SD.Test.Integration.Fixtures
{
    public class TestContext
    {
        public HttpClient Client { get; set; }

        private TestServer _server;

        public TestContext()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
            
        }
    }
}
