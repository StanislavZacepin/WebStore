using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees
        {
            get => Enumerable.Range(1, 10)
.Select(i => new Employee
{
Id = i,
FirstName = $"Иван{i}",
LastName = $"Иванов{i}",
Patronymic = $"Иванович{i}",
Age = i < 4 ? i + 3 : i + 1,
AboutTheEmployee = $"loremi{i + 3}",
}).ToList();
        }
    }
}
