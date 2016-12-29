using System.Web.Mvc;
using Shouldly;
using Xunit;

namespace Recaptcha.UnitTests.GivenAReCaptchaFormSubmission
{
    public class WhenTheReCaptureIsFilledInCorrectly : ReCaptchaAttributeTest
    {
        [Fact]
        public void ThenTheControllerModelStateShouldBeValid()
        {
            _testHttpMessageHandler.Content = "true/test";
            _sut.OnActionExecuting(new ActionExecutingContext());
            _testController.ModelState.IsValid.ShouldBe(true);
        }
    }
}
