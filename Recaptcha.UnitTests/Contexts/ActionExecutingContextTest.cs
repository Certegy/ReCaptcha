﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using NSubstitute;

namespace Recaptcha.UnitTests.Contexts
{
    public class ActionExecutingContextTest
    {
        internal readonly string ExpectedChallengeField = "challenge";
        internal readonly string ExpectedResponseField = "response";

        public ActionExecutingContext ActionExecutingContext { get; private set; }
        public ActionExecutingContextTest()
        {
            var controllerContext = Substitute.For<ControllerContext>();
            var actionDescriptor = Substitute.For<ActionDescriptor>();
            var actionParameters = new Dictionary<string, object>();

            controllerContext.RequestContext.HttpContext.Request.Form.Returns(new NameValueCollection()
                {
                    {"recaptcha_challenge_field", ExpectedChallengeField},
                    {"recaptcha_response_field", ExpectedResponseField}
                });

            ActionExecutingContext =  new ActionExecutingContext(controllerContext, actionDescriptor, actionParameters);
        }
    }
}