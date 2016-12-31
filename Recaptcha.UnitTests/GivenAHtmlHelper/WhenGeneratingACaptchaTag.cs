using System;
using System.Web.Mvc;
using Recaptcha.UnitTests.Contexts;
using ReCaptcha;
using Xunit;
using Shouldly;

namespace Recaptcha.UnitTests.GivenAHtmlHelper
{
    public class WhenGeneratingACaptchaTag : ReCaptchaAttributeTest
    {
        [Theory]
        [InlineData(Theme.BlackGlass)]
        [InlineData(Theme.Red)]
        [InlineData(Theme.White)]
        [InlineData(Theme.Clean)]
        public void ThenItShouldGenerateACorrectlyThemedReCaptchaDiv(Theme theme)
        {
            var htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
            var actual = htmlHelper.GenerateCaptcha(theme, "callback");
            var expected = new MvcHtmlString(
                $"<div id=\"recaptcha_div\" />{Environment.NewLine}" +
                "<script type=\"text/javascript\">" +
                    $"Recaptcha.create(\"{TestSettings.ReCaptchaSiteKey}\", \"recaptcha_div\", {{ theme: \"{theme.ToString().ToLower()}\" , callback: callback}})" +
                "</script>"
            );

            actual.ToString().ShouldBe(expected.ToString());
        }
    }
}
