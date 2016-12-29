using System.Web.Mvc;

namespace ReCaptcha
{
    public class ActionExecutingContextAdapterFactory : IActionExecutingContextAdapterFactory
    {
        public IActionExecutingContextAdapter CreateFrom(ActionExecutingContext filterContext)
        {
            return new DefaultActionExecutingContextAdapter(filterContext);
        }
    }
}