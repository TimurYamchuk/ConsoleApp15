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
            new Department(){ Id = 1, Country = "Ukraine", City = "Odesa"},
            new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
            new Department(){ Id = 3, Country = "France", City = "Paris" },
            new Department(){ Id = 4, Country = "Ukraine", City = "Lviv"}
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

        // 1. Упорядочить имена и фамилии сотрудников по алфавиту, которые проживают в Украине
        var sortedEmployeesInUkraine = (from emp in employees
                                        join dep in departments on emp.DepId equals dep.Id
                                        where dep.Country.Trim() == "Ukraine"
                                        orderby emp.FirstName, emp.LastName
                                        select emp).ToList();

        Console.WriteLine("Сотрудники из Украины (отсортированы по имени и фамилии):");
        foreach (var emp in sortedEmployeesInUkraine)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}");
        }

        // 2. Отсортировать сотрудников по возрастам по убыванию
        var sortedByAgeDesc = employees
            .OrderByDescending(emp => emp.Age)
            .Select(emp => new { emp.Id, emp.FirstName, emp.LastName, emp.Age })
            .ToList();

        Console.WriteLine("\nСотрудники по возрасту (по убыванию):");
        foreach (var emp in sortedByAgeDesc)
        {
            Console.WriteLine($"ID: {emp.Id}, Имя: {emp.FirstName}, Фамилия: {emp.LastName}, Возраст: {emp.Age}");
        }

        // 3. Сгруппировать сотрудников по возрасту и подсчитать количество сотрудников в каждой группе
        var groupedByAge = employees
            .GroupBy(emp => emp.Age)
            .Select(group => new { Age = group.Key, Count = group.Count() })
            .ToList();

        Console.WriteLine("\nГруппировка сотрудников по возрасту:");
        foreach (var group in groupedByAge)
        {
            Console.WriteLine($"Возраст: {group.Age}, Количество сотрудников: {group.Count}");
        }
    }
}
