using System;
using System.Collections.Generic;
using System.Text;

namespace LabEncapsulationDemo
{
    public class Person
    {
        // Privata fält
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _age;

        // Fälten är privata, vilket innebär att de inte kan nås direkt utanför klassen. 
        // För att komma åt och ändra dessa fält använder vi publika egenskaper (properties).
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;  
            LastName = lastName;
            Age = age;
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
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Efternamn kan inte vara tomt eller innehålla endast mellanslag.");
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
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                       nameof(value),
                       "Ålder kan inte vara negativ.");
                }
                _age = value;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} is {Age} years old.";
        }
    }
}
