using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static List<Employee> _Employeescreats => Enumerable.Range(1, 10)
            .Select(i => new Employee
            {
                Id = i,
                FirstName = $"Иван{i}",
                LastName = $"Иванов{i}",
                Patronymic = $"Иванович{i}",
                Age = i % 2==0? i+10 : i+ 15,
            }).ToList();

        public static readonly List<Employee> _Employees = _Employeescreats;
        public IActionResult Index() //http://localhost:5000/Home/Index
        {
            // return Content("Hello †¤││¤†");
            //return View("Index");
            return View();
        }

        public IActionResult SeconAction(string id)
        {
            return Content($"Second action with parameter {id}");
        }

        public IActionResult Employees() //http://localhost:5000/Home/Employees
        {
            return View(_Employees);
        }
    }
}
