using Microsoft.AspNetCore.Mvc;

namespace AccountSecurity.UI.Controllers
{
    [Route("[Controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
