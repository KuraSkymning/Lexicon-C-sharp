using System;
using System.Collections.Generic;
using System.Linq;

namespace LabEncapsulationDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var lines = 5;
            var persons = new List<Person>();
            for (int i = 0; i < lines; i++)
            {
                var cmdArgs = Console.ReadLine().Split();   // Läs in en rad från konsolen och dela upp den i delar baserat på mellanslag

                if (int.TryParse(cmdArgs[2], out int age))
                {
                    try
                    {
                        var person = new Person(
                        cmdArgs[0],
                        cmdArgs[1],
                        age);

                        persons.Add(person);  // Lägg till den skapade personen i listan av personer
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Åldern måste vara ett heltal.");
                }
            }
            persons.OrderBy(p => p.FirstName)   // Sortera listan av personer efter förnamn
                   .ThenBy(p => p.Age)          // Om förnamnen är lika, sortera efter ålder
                   .ToList()                    // Konvertera resultatet till en lista
                   .ForEach(p => Console.WriteLine(p.ToString()));  // Skriv ut varje person i listan till konsolen
                                                                    // För varje person i listan, skriv ut resultatet av p.ToString() till konsolen. 
        }
    }
}
