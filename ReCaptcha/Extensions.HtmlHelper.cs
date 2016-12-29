using System;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ReCaptcha
{
    public static class HtmlHelperExtensions
    {
        private const string RecaptchaCreateScript = @"Recaptcha.create(""{0}"", ""recaptcha_div"", {{ theme: ""{1}"" {2}}})";

        public static MvcHtmlString GenerateCaptcha(this HtmlHelper value, Theme theme, string callBack = null)
        {
            var div = new XElement("div", new XAttribute("id", "recaptcha_div"));
            var script = new XElement("script", new XAttribute("type", "text/javascript"), RecaptchaCreateScript);

            var htmlInjectString = $"{div}{Environment.NewLine}{script}";
            
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