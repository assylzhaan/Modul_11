//Arapova
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M11HW
{
    public interface IComparableEmployee
    {
        int CompareTo(Employee other);
    }

    public struct Employee : IComparableEmployee
    {
        public string FirstName;
        public string LastName;
        public string Position;
        public int[] Date;
        public int Salary;
        public string Gender;

        public int CompareTo(Employee other)
        {
            return String.Compare(LastName, other.LastName, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Position: {Position}, Hire Date: {string.Join("/", Date)}, Salary: {Salary}, Gender: {Gender}";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Number of employees:");
            int numberOfEmployees = int.Parse(Console.ReadLine());

            Employee[] employees = new Employee[numberOfEmployees];


            for (int i = 0; i < numberOfEmployees; i++)
            {
                employees[i] = ReadEmployeeInfo();
            }

            PrintAllEmployeesInfo(employees);

            Console.WriteLine("Position to filter employees:");
            string selectedPosition = Console.ReadLine();
            PrintEmployeesByPosition(employees, selectedPosition);


            PrintManagersAboveAverageClerkSalary(employees);

            Console.WriteLine("Enter the date (format: yyyy/mm/dd):");
            string inputDate = Console.ReadLine();
            DateTime hireDateFilter = DateTime.ParseExact(inputDate, "yyyy/MM/dd", null);
            PrintEmployeesAfterHireDateSorted(employees, hireDateFilter);

            Console.WriteLine("Enter gender (Male/Female/All):");
            string genderFilter = Console.ReadLine();
            PrintEmployeesByGender(employees, genderFilter);
        }

        static Employee ReadEmployeeInfo()
        {
            Console.WriteLine("First name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Position:");
            string position = Console.ReadLine();

            Console.WriteLine("Date (format: yyyy/mm/dd):");
            int[] hireDate = Array.ConvertAll(Console.ReadLine().Split('/'), int.Parse);

            Console.WriteLine("Salary:");
            int salary = int.Parse(Console.ReadLine());

            Console.WriteLine("Gender (Male/Female):");
            string gender = Console.ReadLine();

            return new Employee { FirstName = firstName, LastName = lastName, Position = position, Date = hireDate, Salary = salary, Gender = gender };
        }

        static void PrintAllEmployeesInfo(Employee[] employees)
        {
            Console.WriteLine("Full information about all employees:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesByPosition(Employee[] employees, string position)
        {
            Console.WriteLine($"Full information about employees with position '{position}':");
            foreach (var employee in employees)
            {
                if (employee.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(employee);
                }
            }
            Console.WriteLine();
        }


        static void PrintManagersAboveAverageClerkSalary(Employee[] employees)
        {
            var clerks = employees.Where(e => e.Position.Equals("Clerk", StringComparison.OrdinalIgnoreCase));
            double averageClerkSalary = clerks.Any() ? clerks.Average(e => e.Salary) : 0;

            Console.WriteLine($"Managers with salary greater than average clerk salary ({averageClerkSalary}), sorted by last name:");

            var managers = employees.Where(e => e.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase) && e.Salary > averageClerkSalary)
                                    .OrderBy(e => e.LastName);

            foreach (var manager in managers)
            {
                Console.WriteLine(manager);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesAfterHireDateSorted(Employee[] employees, DateTime hireDateFilter)
        {
            Console.WriteLine($"Employees hired after {hireDateFilter.ToString("yyyy/MM/dd")}, sorted by last name:");

            var filteredEmployees = employees.Where(e => new DateTime(e.Date[0], e.Date[1], e.Date[2]) > hireDateFilter)
                                             .OrderBy(e => e.LastName);

            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        static void PrintEmployeesByGender(Employee[] employees, string genderFilter)
        {
            Console.WriteLine($"Employees with gender '{genderFilter}':");

            var filteredEmployees = employees.Where(e => genderFilter.Equals("All", StringComparison.OrdinalIgnoreCase) || e.Gender.Equals(genderFilter, StringComparison.OrdinalIgnoreCase));

            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine(employee);
            }
            Console.ReadKey();

        }
    }

}
