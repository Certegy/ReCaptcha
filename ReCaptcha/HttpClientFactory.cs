namespace ReCaptcha
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public IHttpClient CreateHttpClient()
        {
            return new HttpClientAdapter();
        }
    }
}