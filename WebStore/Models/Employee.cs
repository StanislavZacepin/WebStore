﻿namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }
    
        public int Age { get; set; }

        public string AboutTheEmployee { get; set; }
    }
    // public record Employee2(int Id, string LastName, string FirstName, string Patronymic, int Age, string AboutTheEmployee)
}