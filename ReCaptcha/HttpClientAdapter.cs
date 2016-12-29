using System.Net.Http;
using System.Threading.Tasks;

namespace ReCaptcha
{
    public class HttpClientAdapter : IHttpClient
    {
        private readonly HttpClient wrapped;

        public HttpClientAdapter()
        {
            wrapped = new HttpClient();
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return wrapped.PostAsync(requestUri, content);
        }
    }
}