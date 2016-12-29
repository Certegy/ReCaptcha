﻿using System.Web.Mvc;
using Shouldly;
using Xunit;

namespace Recaptcha.UnitTests.GivenAReCaptchaFormSubmission
{
    public class WhenTheReCaptureIsNotFilledInCorrectly : ReCaptchaAttributeTest
    {
        [Fact]
        public void ThenTheControllerModelStateShouldBeValid()
        {
            _testHttpMessageHandler.Content = "false/test";
            _sut.OnActionExecuting(new ActionExecutingContext());
            _testController.ModelState.IsValid.ShouldBe(false);
        }
    }
}
