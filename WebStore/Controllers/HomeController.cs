using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index() => View();

        public IActionResult Status(string id)
        {
            switch (id)
            {
                default: return Content($"Status --- {id}");
                case "404": return View("Error404");

               
                    
            }
            
        }
    }
}
