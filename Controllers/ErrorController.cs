using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlackSquares.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error/404")]
        public new IActionResult NotFound()
        {
            return new NotFoundViewResult();
        }
    }

    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult()
        {
            ViewName = "NotFound";
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
