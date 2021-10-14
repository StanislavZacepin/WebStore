using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _Logger;

        private int _CurrentMaxId;

        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger)
        {
            _Logger = Logger;
            _CurrentMaxId = TestData.Employees.Max(e => e.Id);
        }

        public void Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee))
                _Logger.LogInformation("Такой сотрудник уже есть");
            else
            {
                employee.Id = ++_CurrentMaxId;
                TestData.Employees.Add(employee);
            }         
                        
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null) return false;

            TestData.Employees.Remove(db_employee);

            return true;
        }

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee GetById(int id) => TestData.Employees.SingleOrDefault(e => e.Id == id);

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee)) return;// толко для реализации в памяти!!

            var db_employee = GetById(employee.Id);
            if (db_employee is null) return;

            db_employee.LastName = employee.LastName;
            db_employee.FirstName = employee.FirstName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
            db_employee.AboutTheEmployee = employee.AboutTheEmployee;

            //db.SaveChanges();

        }
    }
}
