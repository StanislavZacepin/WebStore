using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _EmployeesData.GetAll();
            return Ok(TestData.Employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return Ok(employee);
        }

        [HttpGet("count")]
        public IActionResult Cont() => Ok(_EmployeesData);

        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add(Employee employee)
        {

            var id = _EmployeesData.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id }, employee);

            //return Ok(id);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            _EmployeesData.Update(employee);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _EmployeesData.Delete(id);

            return result ? Ok(true) : NotFound(false);
        }
    }
}
