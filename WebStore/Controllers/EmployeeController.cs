using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        public static Employee _Employee { get; set; }

       
       
       
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
