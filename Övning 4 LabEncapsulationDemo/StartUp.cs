using System;
using System.Collections.Generic;

namespace LabEncapsulationDemo
{
    public class StartUp
    {
        public static void Main()
        {
            int lines = int.Parse(Console.ReadLine() ?? "0");
            List<Person> persons = [];
            for (int i = 0; i < lines; i++)
            {
                string[] cmdArgs = (Console.ReadLine() ?? "").Split();
                try
                {
                    Person person = new(
                        cmdArgs[0],
                        cmdArgs[1],
                        int.Parse(cmdArgs[2]),
                        decimal.Parse(cmdArgs[3]
                    ));
                    persons.Add(person);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            if (!decimal.TryParse(Console.ReadLine(), out decimal bonus))
            {
                Console.WriteLine("Felaktig bonus.");
            }
            persons.ForEach(p => p.IncreaseSalary(bonus));
            persons.ForEach(p => Console.WriteLine(p.ToString()));
        }
    }
}
