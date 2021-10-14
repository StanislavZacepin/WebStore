using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;
using WebStore.Services.Data;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        

        [HttpGet]
        public IActionResult Get() => Ok(TestData.Employees);

        [HttpGet("{Id}")]
        public IActionResult GetId(int Id)
        {
            if (!TestData.Employees.Contains(TestData.Employees[Id]))
                return NotFound();

            return Ok(TestData.Employees[Id]);
        }

        [HttpGet("count")]
        public IActionResult Cont() => Ok(TestData.Employees.Count);

        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add([FromBody] Employee Value)
        {

            TestData.Employees.Add(Value);



            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult Replace(int Id, [FromBody] Employee employee)
        {
            if (!TestData.Employees.Contains(TestData.Employees[Id]))
                return NotFound();

            TestData.Employees[Id] = employee;

            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (!TestData.Employees.Contains(TestData.Employees[Id]))
                return NotFound();

            TestData.Employees.RemoveAt(Id);

            return Ok();
        }
    }
}
