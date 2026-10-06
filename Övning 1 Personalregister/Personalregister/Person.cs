namespace Personalregister
{
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
