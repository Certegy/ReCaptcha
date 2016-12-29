using System.Web.Mvc;

namespace ReCaptcha
{
    public interface IActionExecutingContextAdapterFactory
    {
        IActionExecutingContextAdapter CreateFrom(ActionExecutingContext filterContext);
    }
}