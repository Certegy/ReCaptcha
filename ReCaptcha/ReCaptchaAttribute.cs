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

        private readonly Func<ActionExecutingContext, IActionExecutingContextAdapter> _actionExecutingContextAdapterFunc;
        private readonly Func<HttpClient> _httpClientFunc;

        public ReCaptchaAttribute() : this(context => new DefaultActionExecutingContextAdapter(context), () => new HttpClient())
        {
        }

        public ReCaptchaAttribute(
            Func<ActionExecutingContext, IActionExecutingContextAdapter> actionExecutingContextAdapterFunc,
            Func<HttpClient> httpClientFunc)
        {
            _actionExecutingContextAdapterFunc = actionExecutingContextAdapterFunc;
            _httpClientFunc = httpClientFunc;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var reCaptchaUrl = ReCaptchaResourceSettingsLocator.Get(settings => settings?.ReCaptchaUrl);
            var reCaptchaSecretKey = ReCaptchaResourceSettingsLocator.Get(settings => settings?.ReCaptchaSecretKey);

            if (string.IsNullOrWhiteSpace(reCaptchaSecretKey))
                throw new Exception("Missing ReCaptcha SecretKey");

            var actionExecutingContextAdapter = _actionExecutingContextAdapterFunc.Invoke(filterContext);

            var postData = 
                $"&privatekey={reCaptchaSecretKey}" +
                $"&remoteip={reCaptchaUrl}" +
                $"&challenge={actionExecutingContextAdapter.ReCaptchaChallengeField}" +
                $"&response={actionExecutingContextAdapter.ReCaptchaResponseField}";

            var postDataAsBytes = Encoding.UTF8.GetBytes(postData);

            // Create web request
            var httpClient = _httpClientFunc.Invoke();
            
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