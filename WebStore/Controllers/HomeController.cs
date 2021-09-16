using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

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
        public IActionResult Employees()
        {
            return View(EmployeesController.Employees);
        }

        
        public IActionResult Employee()
        {
            return View(EmployeeController._Employee);
        }
       
       
        

        //public string Employee(Employee employee)
        //{
        //    return $"ID{employee.Id} Имя:{employee.LastName} Фамилия:{employee.FirstName} Отчество:{employee.Patronymic} Лет:{employee.Age} О сотруднике {employee.AboutTheEmployee}";
        //}



    }
}
