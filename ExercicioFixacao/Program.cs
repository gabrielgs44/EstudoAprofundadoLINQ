using ExercicioFixacao.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExercicioFixacao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> list = new List<Employee>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double salaryEmployee = double.Parse(fields[2], CultureInfo.InvariantCulture);

                    list.Add(new Employee(name, email, salaryEmployee));
                }
            }

            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();

            var emails = list.
                Where(e=>e.Salary > salary)
                .OrderBy(e=>e.Email)
                .Select(e => e.Email);
            Console.WriteLine("Email of people whose salary is more than " + salary.ToString("F2", CultureInfo.InvariantCulture));
            foreach(string e in emails)
            {
                Console.WriteLine(e);
            }

            var avg = list
                .Where(e => e.Name[0] == 'M')
                .Select(e=>e.Salary)
                .DefaultIfEmpty(0.0)
                .Sum();

            Console.WriteLine("Sum of salary of people whose name start with 'M': " + avg.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
