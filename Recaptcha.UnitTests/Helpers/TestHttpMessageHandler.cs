using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Recaptcha.UnitTests.Helpers
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        public TestHttpMessageHandler() : this(null)
        {
        }

        public TestHttpMessageHandler(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(Content)
            };

            return await Task.FromResult(responseMessage);
        }
    }
}