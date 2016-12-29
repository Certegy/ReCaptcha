using System.Net.Http;
using System.Threading.Tasks;

namespace ReCaptcha
{
    public class HttpClientAdapter : IHttpClient
    {
        private readonly HttpClient _wrapped;

        public HttpClientAdapter()
        {
            _wrapped = new HttpClient();
        }

        public HttpClientAdapter(HttpMessageHandler handler)
        {
            _wrapped = new HttpClient(handler);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return _wrapped.PostAsync(requestUri, content);
        }
    }
}