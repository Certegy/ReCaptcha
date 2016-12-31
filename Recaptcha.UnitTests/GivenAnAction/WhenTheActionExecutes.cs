using Recaptcha.UnitTests.Contexts;
using ReCaptcha;
using Shouldly;
using Xunit;

namespace Recaptcha.UnitTests.GivenAnAction
{
    public class WhenTheActionExecutes : ActionExecutingContextTest
    {
        [Fact]
        public void ThenItShouldAddSomeDataToTheExecutionContext()
        {
            var adapter = new DefaultActionExecutingContextAdapter(ActionExecutingContext);

            adapter.ReCaptchaChallengeField.ShouldBe(ExpectedChallengeField); 
            adapter.ReCaptchaResponseField.ShouldBe(ExpectedResponseField);
        }
    }
}
