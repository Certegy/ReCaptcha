namespace ReCaptcha
{
    public interface IHttpClientFactory
    {
        IHttpClient CreateHttpClient();
    }
}