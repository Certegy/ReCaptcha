using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;

namespace ReCaptcha
{
    public class ReCaptchaAttribute : ActionFilterAttribute
    {
        private const string RecaptchaApi = "http://www.google.com/recaptcha/api/verify";

        private readonly IActionExecutingContextAdapterFactory _actionExecutingContextAdapterFactory;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReCaptchaAttribute() : this(new ActionExecutingContextAdapterFactory(), new HttpClientFactory())
        {
        }

        public ReCaptchaAttribute(
            IActionExecutingContextAdapterFactory actionExecutingContextAdapterFactory,
            IHttpClientFactory httpClientFactory)
        {
            _actionExecutingContextAdapterFactory = actionExecutingContextAdapterFactory;
            _httpClientFactory = httpClientFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var reCaptchaUrl = ReCaptchaResourceSettingsLocator.Get(settings => settings?.ReCaptchaUrl);
            var reCaptchaSecretKey = ReCaptchaResourceSettingsLocator.Get(settings => settings?.ReCaptchaSecretKey);

            if (string.IsNullOrWhiteSpace(reCaptchaSecretKey))
                throw new Exception("Missing ReCaptcha SecretKey");

            var actionExecutingContextAdapter = _actionExecutingContextAdapterFactory.CreateFrom(filterContext);

            var postData = 
                $"&privatekey={reCaptchaSecretKey}" +
                $"&remoteip={reCaptchaUrl}" +
                $"&challenge={actionExecutingContextAdapter.ReCaptchaChallengeField}" +
                $"&response={actionExecutingContextAdapter.ReCaptchaResponseField}";

            var postDataAsBytes = Encoding.UTF8.GetBytes(postData);

            // Create web request
            var httpClient = _httpClientFactory.CreateHttpClient();

            var content = new ByteArrayContent(postDataAsBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.ContentLength = postDataAsBytes.Length;
            var requestTask =
                httpClient
                    .PostAsync(RecaptchaApi, content)
                    .ContinueWith(response =>
                    {
                        var responseTask = response.Result.Content.ReadAsStringAsync();
                        responseTask.Wait();

                        if (!responseTask.Result.StartsWith("true"))
                        {
                            actionExecutingContextAdapter.Controller?.ModelState.AddModelError("ReCaptcha", @"Captcha words typed incorrectly");
                        }
                    });

            requestTask.Wait();
        }
    }
}