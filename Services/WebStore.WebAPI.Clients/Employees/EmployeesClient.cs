using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient , IEmployeesData
    {
        public EmployeesClient(HttpClient Client) : base(Client, "api/Employees")
        {

        }
       
        public int Count()
        {
            var response = Http.GetAsync($"{Address}/count").Result;
            if( response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<int>().Result;

            return -1;

        }
        public void Add(Employee employee)
        {
            var response = Http.PostAsJsonAsync(Address, employee).Result;
            response.EnsureSuccessStatusCode();


        }

        public bool Delete(int Id)
        {
            var response = Http.DeleteAsync($"{Address}/{Id}").Result;
            return response.IsSuccessStatusCode;
        }

        public IEnumerable<Employee> GetAll()
        {
            var response = Http.GetAsync(Address).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<IEnumerable<Employee>>().Result;

            return Enumerable.Empty<Employee>();

        }

        public Employee GetById(int id)
        {
            var response = Http.GetAsync($"{Address}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<Employee>().Result;

            return null;
        }

        public void Update(Employee employee)
        {
            var response = Http.PutAsJsonAsync($"{Address}/{employee.Id}", employee).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
