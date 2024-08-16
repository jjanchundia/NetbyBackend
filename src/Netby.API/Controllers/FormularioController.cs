using Microsoft.AspNetCore.Mvc;

namespace Netby.API.Controllers
{
    public class FormularioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
