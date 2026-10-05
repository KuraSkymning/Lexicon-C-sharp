namespace Personalregister
{
    internal class Program
    {
        static void Main()
        {
            Register register = new();

            MenuItem[] menuItems =
            [
                new MenuItem("1", "Visa personalregistret", ()=> OutputToConsole(register)),
                new MenuItem("2", "Lägg till person", ()=> AddPersonFromConsole(register)),
                new MenuItem("3", "Ta bort person", ()=> RemovePersonFromConsole(register)),
                new MenuItem("x", "Avsluta", null),
            ];

            Console.WriteLine("Välkommen till personalregistret");

            try
            {
                while (true)
                {
                    Console.WriteLine("\nMeny:\n");
                    foreach (MenuItem item in menuItems)
                        Console.WriteLine($"{item.label} - {item.title}");
                    Console.Write("\nAnge val: ");

                    string label = Console.ReadLine() ?? throw new IOException();
                    int n = Array.FindIndex(menuItems, item => item.label == label);
                    if (n >= 0)
                    {
                        Action? action = menuItems[n].action;
                        if (action == null)
                        {
                            Console.WriteLine("Programmet avslutas.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine(menuItems[n].title);
                            action();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Du måste ange ett korrekt menyval.");
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Programmet avslutas på grund av slut på indata eller IO-fel.");
                return;
            }
        }

        static internal void OutputToConsole(Register register, bool showId = false)
        {
            if (register.IsEmpty())
            {
                Console.WriteLine("Personalregistret är tomt.");
            }
            else
            {
                register.OutputToConsole(showId);
            }
        }

        static internal void AddPersonFromConsole(Register register)
        {
            string firstName;
            while (true)
            {
                Console.Write("Ange förnamn: ");
                firstName = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (firstName.Length == 0)
                    Console.WriteLine("Du måste ange ett förnamn.");
                else
                    break;
            }

            string lastName;
            while (true)
            {
                Console.Write("Ange efternamn: ");
                lastName = Console.ReadLine()?.Trim() ?? throw new IOException();
                if (lastName.Length == 0)
                    Console.WriteLine("Du måste ange ett efternamn.");
                else
                    break;
            }

            decimal salary;
            while (true)
            {
                Console.Write("Ange lön: ");
                string s = Console.ReadLine() ?? throw new IOException();
                if (!decimal.TryParse(s, out salary) || salary < 0)
                    Console.WriteLine("Du måste ange en korrekt lön.");
                else
                    break;
            }

            Person p = register.AddPerson(firstName, lastName, salary);
            Console.WriteLine($"Du har lagt till {p} i personalregistret.");
        }

        internal static void RemovePersonFromConsole(Register register)
        {
            if (register.IsEmpty())
            {
                Console.WriteLine("Personalregistret är tomt.");
                return;
            }

            Console.WriteLine("Personer:");
            register.OutputToConsole(true);
            Console.WriteLine("\nFör att ta bort en person ange personens id, eller ange x för att avbryta.");

            while (true)
            {
                Console.Write("Ange id: ");
                string id = Console.ReadLine() ?? throw new IOException();
                if (id == "x")
                {
                    Console.WriteLine("Avbryter.");
                    return;
                }
                else if (int.TryParse(id, out int pid))
                {
                    Person? person = register.RemovePerson(pid);
                    if (person != null)
                    {
                        Console.WriteLine($"Du har tagit bort {person} ur personalregistret.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Det finns ingen person med id {id}.");
                    }
                }

                Console.WriteLine("Du måste ange ett korrekt id.");
            }
        }
    }

    internal readonly struct MenuItem(string label, string title, Action? action)
    {
        internal readonly string label = label;
        internal readonly string title = title;
        internal readonly Action? action = action;
    }

    public class Register
    {
        private readonly List<Person> persons = [];
        private int idCounter = 0;

        public Person AddPerson(string firstName, string lastName, decimal salary)
        {
            checked
            {
                idCounter++;
            }
            Person person = new(idCounter, firstName, lastName, salary);
            persons.Add(person);
            return person;
        }

        public Person? RemovePerson(int id)
        {
            int n = persons.FindIndex(p => p.Id == id);
            if (n >= 0)
            {
                Person person = persons[n];
                persons.RemoveAt(n);
                return person;
            }
            else
            {
                return null;
            }
        }

        public bool IsEmpty()
        {
            return persons.Count == 0;
        }

        public void OutputToConsole(bool showId = false)
        {
            foreach (Person person in persons.OrderBy(p => p.FirstName + p.LastName))
            {
                Console.WriteLine($"\n- {person}");
                if (showId)
                    Console.WriteLine($"  Id: {person.Id}");
                Console.WriteLine($"  Lön: {person.Salary:C}");
            }
        }
    }

    public class Person(int id, string firstName, string lastName, decimal salary)
    {
        public int Id { get; init; } = id;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public decimal Salary { get; set; } = salary;

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
