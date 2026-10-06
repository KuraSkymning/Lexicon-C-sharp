namespace Personalregister
{
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
}
