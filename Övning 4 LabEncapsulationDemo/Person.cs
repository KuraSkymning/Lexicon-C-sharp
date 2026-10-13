using System;

namespace LabEncapsulationDemo
{
    public class Person
    {
        private string _firstName = "";
        private string _lastName = "";
        private int _age;
        private decimal _salary;

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Person(string firstName, string lastName, int age, decimal salary) : this(firstName, lastName, age)
        {
            Salary = salary;
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Förnamn kan inte vara tomt eller innehålla endast mellanslag.");
                }
                if (value.Length < 3)
                {
                    throw new ArgumentException("Förnamn får inte innehålla färre än 3 tecken.");
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Efternamn kan inte vara tomt eller innehålla endast mellanslag.");
                }
                if (value.Length < 3)
                {
                    throw new ArgumentException("Efternamn får inte innehålla färre än 3 tecken.");
                }
                _lastName = value;
            }
        }


        public int Age
        {
            get
            {
                return _age;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Ålder kan inte vara noll eller negativ.");
                }
                _age = value;
            }
        }

        public decimal Salary
        {
            get
            {
                return _salary;
            }
            private set
            {
                if (value < 460)
                {
                    throw new ArgumentException($"Lön kan inte vara lägre än {460:C}.");
                }
                _salary = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            if (Age < 30)
            {
                percentage /= 2;
            }
            Salary += Salary * percentage / 100;
        }

        public override string ToString()
        {
            //return $"{FirstName} {LastName} är {Age} år gammal.";
            return $"{FirstName} {LastName} tjänar {Salary:C}.";
        }
    }
}
