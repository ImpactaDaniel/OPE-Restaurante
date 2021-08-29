using Microsoft.AspNetCore.Mvc;

namespace Restaurante.Web.Controllers
{
    public abstract class WebControllerBase : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
