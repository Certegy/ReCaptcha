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

        private readonly IHttpClientFactory httpClientFactory;

        public ReCaptchaAttribute() : this(new HttpClientFactory())
        {
        }

        public ReCaptchaAttribute(
            IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var reCaptchaUrl = ReCaptchaResourceSettingsLocator.Get(settings => settings.ReCaptchaUrl);
            var reCaptchaSecretKey = ReCaptchaResourceSettingsLocator.Get(settings => settings.ReCaptchaSecretKey);

            if (string.IsNullOrWhiteSpace(reCaptchaSecretKey))
                throw new Exception("Missing ReCaptcha SecretKey");

            var reCaptchaChallengeField =
                filterContext.RequestContext.HttpContext.Request.Form["recaptcha_challenge_field"];
            var reCaptchaResponseField =
                filterContext.RequestContext.HttpContext.Request.Form["recaptcha_response_field"];

            var postData =
                $"&privatekey={reCaptchaSecretKey}&remoteip={reCaptchaUrl}&challenge={reCaptchaChallengeField}&response={reCaptchaResponseField}";

            var postDataAsBytes = Encoding.UTF8.GetBytes(postData);

            // Create web request
            var httpClient = httpClientFactory.CreateHttpClient();

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
                            ((Controller) filterContext.Controller).ModelState.AddModelError("ReCaptcha",
                                @"Captcha words typed incorrectly");
                    });

            requestTask.Wait();
        }
    }
}