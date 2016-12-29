using System.Web.Mvc;

namespace ReCaptcha
{
    public interface IActionExecutingContextAdapter
    {
        string ReCaptchaChallengeField { get; }
        string ReCaptchaResponseField { get; }
        Controller Controller { get; }
    }
}