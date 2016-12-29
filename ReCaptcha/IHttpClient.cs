using System.Net.Http;
using System.Threading.Tasks;

namespace ReCaptcha
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}