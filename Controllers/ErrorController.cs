using Microsoft.AspNetCore.Mvc;

namespace Condusef.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int? code)
        {
            if (code.HasValue && (code >= 200 && code < 400))
            {
                // Códigos 2xx y 3xx se consideran éxito o redirección
                // Puedes redirigir a una acción predeterminada o hacer otro manejo específico
                return RedirectToAction("SuccessOrRedirection", "Home");
            }
            if(code >= 500) return RedirectToAction("ServerError", "Error");
            switch (code)
            {
                case 401:
                    return RedirectToAction("AccessDenied", "Error");
                case 403:
                    return RedirectToAction("AccessDenied", "Error");
                default:
                    return RedirectToAction("NotFound", "Error");
            }
        }

        [Route("error/not-found", Name = "NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }

        [Route("error/server-error", Name = "ServerError")]
        public IActionResult ServerError()
        {
            return View();
        }

        [Route("error/access-denied", Name = "AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}
