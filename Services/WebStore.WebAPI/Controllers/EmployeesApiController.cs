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
    ///<summary> Управление сотрудниками </summary>
    [Route(WebAPIAddresses.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;
        /// <summary> Получение всех сотрудников </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _EmployeesData.GetAll();
            return Ok(TestData.Employees);
        }

        /// <summary> Получение сотрудника по его идентификатору </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник с указанным идентификатором</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return Ok(employee);
        }

        /// <summary> Количество Сотрудников </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult Cont() => Ok(_EmployeesData);

        /// <summary> Добавление сотрудника </summary>
        /// <param name="employee">Присваивание Ид сотруднику</param>
        /// <returns>Добавления сотрудника в базу</returns>
        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add(Employee employee)
        {

            var id = _EmployeesData.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id }, employee);

            //return Ok(id);
        }

        /// <summary> Обнавление данных сотрудника </summary>
        /// <param name="employee">Обзнавление данных</param>
        /// <returns>Присваивание обнавленных данных</returns>
        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            _EmployeesData.Update(employee);

            return Ok();
        }

        /// <summary> Удаление сотрудника </summary>
        /// <param name="id">получение ИД сотрудника</param>
        /// <returns>Удаление по ИД сотрудника</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _EmployeesData.Delete(id);

            return result ? Ok(true) : NotFound(false);
        }
    }
}
