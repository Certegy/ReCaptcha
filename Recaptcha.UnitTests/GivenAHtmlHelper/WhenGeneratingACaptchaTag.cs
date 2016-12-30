using System;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;
using NSubstitute;
using Recaptcha.UnitTests.Contexts;
using Recaptcha.UnitTests.GivenAReCaptchaFormSubmission;
using Recaptcha.UnitTests.Helpers;
using ReCaptcha;
using Xunit;
using Shouldly;

namespace Recaptcha.UnitTests.GivenAHtmlHelper
{
    public class WhenGeneratingACaptchaTag : ReCaptchaAttributeTest
    {
        [Theory]
        [InlineData(Theme.BlackGlass, "blackglass")]
        [InlineData(Theme.Red, "red")]
        [InlineData(Theme.White, "white")]
        [InlineData(Theme.Clean, "clean")]
        public void ThenItShouldGenerateACorrectlyThemedReCaptchaDiv(Theme t, string theme)
        {
            var htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
            var actual = htmlHelper.GenerateCaptcha(t, "callback");
            var expected = new MvcHtmlString(
                $"<div id=\"recaptcha_div\" />{Environment.NewLine}" +
                "<script type=\"text/javascript\">" +
                    $"Recaptcha.create(\"{TestSettings.ReCaptchaSiteKey}\", \"recaptcha_div\", {{ theme: \"{theme}\" , callback: callback}})" +
                "</script>"
            );

            actual.ToString().ShouldBe(expected.ToString());
        }
    }
}
