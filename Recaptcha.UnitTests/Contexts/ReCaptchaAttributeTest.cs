﻿using System.Net.Http;
using NSubstitute;
using Recaptcha.UnitTests.Helpers;
using ReCaptcha;

namespace Recaptcha.UnitTests.Contexts
{
    public abstract class ReCaptchaAttributeTest
    {
        protected readonly ReCaptchaAttribute _sut;
        protected readonly TestController _testController;
        protected readonly TestHttpMessageHandler _testHttpMessageHandler;
        internal TestReCaptchaResourceSettings TestSettings { get; private set; }

        protected ReCaptchaAttributeTest()
        {
            TestSettings = new TestReCaptchaResourceSettings();
            ReCaptchaResourceSettingsLocator.Register(TestSettings);
            var actionExecutingContextAdapter = Substitute.For<IActionExecutingContextAdapter>();
            actionExecutingContextAdapter.ReCaptchaChallengeField.Returns("0AA41D54-DA6D-4BA1-9BF0-81AB478B663F");
            actionExecutingContextAdapter.ReCaptchaResponseField.Returns("71A4C469-8037-446F-A6F4-0114DDFE6984");
            _testController = new TestController();
            actionExecutingContextAdapter.Controller.Returns(_testController);
            _testHttpMessageHandler = new TestHttpMessageHandler();
            _sut = new ReCaptchaAttribute(context => actionExecutingContextAdapter, () => new HttpClient(_testHttpMessageHandler));
        }
    }
}