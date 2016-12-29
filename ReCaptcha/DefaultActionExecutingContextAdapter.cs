using System.Web.Mvc;

namespace ReCaptcha
{
    public class DefaultActionExecutingContextAdapter : IActionExecutingContextAdapter
    {
        private readonly ActionExecutingContext _wrapped;

        public DefaultActionExecutingContextAdapter(ActionExecutingContext filterContext)
        {
            _wrapped = filterContext;
        }

        public string ReCaptchaChallengeField => _wrapped.RequestContext.HttpContext.Request.Form["recaptcha_challenge_field"];
        public string ReCaptchaResponseField => _wrapped.RequestContext.HttpContext.Request.Form["recaptcha_response_field"];
        public Controller Controller => (Controller) _wrapped.Controller;
    }
}