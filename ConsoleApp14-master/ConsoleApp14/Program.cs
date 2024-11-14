using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int DepId { get; set; }
}

class Department
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}

class Program
{
    static void Main()
    {
        List<Department> departments = new List<Department>()
        {
            new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
            new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
            new Department(){ Id = 3, Country = "France", City = "Paris" },
            new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
        };

        List<Employee> employees = new List<Employee>()
        {
            new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
            new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
            new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
            new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
            new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
            new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
            new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
        };

        // 1) Выбрать имена и фамилии сотрудников, работающих в Украине, но не в Одессе
        var employeesInUkraineNotOdesa = employees
            .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
            .Where(x => x.dep.Country.Trim() == "Ukraine" && x.dep.City != "Odesa")
            .Select(x => new { x.emp.FirstName, x.emp.LastName })
            .ToList();

        Console.WriteLine("Сотрудники, работающие в Украине, но не в Одессе:");
        foreach (var emp in employeesInUkraineNotOdesa)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}");
        }

        // 2) Вывести список стран без повторений
        var uniqueCountries = departments
            .Select(dep => dep.Country.Trim())
            .Distinct()
            .ToList();

        Console.WriteLine("\nСписок стран без повторений:");
        foreach (var country in uniqueCountries)
        {
            Console.WriteLine(country);
        }

        // 3) Выбрать 3-x первых сотрудников, возраст которых превышает 25 лет
        var firstThreeAbove25 = employees
            .Where(emp => emp.Age > 25)
            .Take(3)
            .Select(emp => new { emp.FirstName, emp.LastName, emp.Age })
            .ToList();

        Console.WriteLine("\nТри первых сотрудника, чей возраст превышает 25 лет:");
        foreach (var emp in firstThreeAbove25)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Возраст: {emp.Age}");
        }

        // 4) Выбрать имена, фамилии и возраст студентов из Киева, возраст которых превышает 23 года
        var employeesInKyivAbove23 = employees
            .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
            .Where(x => x.dep.City == "Kyiv" && x.emp.Age > 23)
            .Select(x => new { x.emp.FirstName, x.emp.LastName, x.emp.Age })
            .ToList();

        Console.WriteLine("\nСотрудники из Киева, чей возраст превышает 23 года:");
        foreach (var emp in employeesInKyivAbove23)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Возраст: {emp.Age}");
        }
    }
}
