using System.Web.Mvc;
using NSubstitute;
using Recaptcha.UnitTests.Helpers;
using ReCaptcha;

namespace Recaptcha.UnitTests.GivenAReCaptchaFormSubmission
{
    public abstract class ReCaptchaAttributeTest
    {
        protected readonly ReCaptchaAttribute _sut;
        protected readonly TestController _testController;
        protected readonly TestHttpMessageHandler _testHttpMessageHandler;

        protected ReCaptchaAttributeTest()
        {
            ReCaptchaResourceSettingsLocator.Register(new TestReCaptchaResourceSettings());
            var actionExecutingContextAdapterFactory = Substitute.For<IActionExecutingContextAdapterFactory>();
            var actionExecutingContextAdapter = Substitute.For<IActionExecutingContextAdapter>();
            actionExecutingContextAdapter.ReCaptchaChallengeField.Returns("0AA41D54-DA6D-4BA1-9BF0-81AB478B663F");
            actionExecutingContextAdapter.ReCaptchaResponseField.Returns("71A4C469-8037-446F-A6F4-0114DDFE6984");
            _testController = new TestController();
            actionExecutingContextAdapter.Controller.Returns(_testController);
            actionExecutingContextAdapterFactory.CreateFrom(Arg.Any<ActionExecutingContext>()).Returns(actionExecutingContextAdapter);
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            _testHttpMessageHandler = new TestHttpMessageHandler();
            IHttpClient httpClient = new HttpClientAdapter(_testHttpMessageHandler);
            httpClientFactory.CreateHttpClient().Returns(httpClient);
            _sut = new ReCaptchaAttribute(actionExecutingContextAdapterFactory, httpClientFactory);
        }
    }
}