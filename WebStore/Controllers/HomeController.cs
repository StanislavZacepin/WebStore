using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello †¤││¤†");
        }

        public IActionResult SeconAction(string id)
        {
            return Content($"Second action with parameter {id}");
        }
    }
}
