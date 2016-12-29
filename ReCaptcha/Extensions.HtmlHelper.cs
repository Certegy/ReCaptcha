using System;
using System.Web.Mvc;

namespace ReCaptcha
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper value, Theme theme, string callBack = null)
        {
            const string htmlInjectString = @"<div id=""recaptcha_div""></div>
                <script type=""text/javascript"">
                    Recaptcha.create(""{0}"", ""recaptcha_div"", {{ theme: ""{1}"" {2}}});
                </script>";
            
            var publicKey = ReCaptchaResourceSettingsLocator.Get(settings => settings.ReCaptchaSiteKey);

            if (string.IsNullOrWhiteSpace(publicKey))
                throw new Exception("Missing ReCaptcha SiteKey");

            if (!string.IsNullOrWhiteSpace(callBack))
                callBack = string.Concat(", callback: ", callBack);

            var html = string.Format(htmlInjectString, publicKey, theme.ToString().ToLower(), callBack);
            return MvcHtmlString.Create(html);
        }
    }
}