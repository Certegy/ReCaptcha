using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using NSubstitute;
using Recaptcha.UnitTests.Contexts;
using ReCaptcha;
using Shouldly;
using Xunit;

namespace Recaptcha.UnitTests.GivenAnExecutingAction
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
