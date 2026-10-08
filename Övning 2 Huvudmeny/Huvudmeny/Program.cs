namespace Huvudmeny
{
    internal class Program
    {

        /*
         * Visar en huvudmeny med olika val som användaren kan välja
         * för att utföra olika funktioner.
         * Menyn visas om igen efter att varje funktion har utförts,
         * tills dess att användaren väljer att avsluta programmet.
         * Tar emot indata från stdin och skriver ut till stdout.
         */
        static void Main()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Huvudmeny");
                Console.WriteLine("Ange en siffra och tryck på enter för att göra ett menyval.\n");
                Console.WriteLine("1. Visa aktuellt biljettpris beroende på en persons ålder");
                Console.WriteLine("2. Beräkna totalpriset för biljetter för ett sällskap");
                Console.WriteLine("3. Upprepa en text tio gånger");
                Console.WriteLine("4. Välj ut det tredje ordet från en text");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nAnge val: ");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.WriteLine("Avslutar programmet.");
                        loop = false;
                        break;
                    case "1":
                        CheckAgeAndShowPrice();
                        break;
                    case "2":
                        CalculatePriceForGroup();
                        break;
                    case "3":
                        RepeatTenTimes();
                        break;
                    case "4":
                        SelectThirdWord();
                        break;
                    default:
                        Console.WriteLine("Felaktigt val. Du måste ange en av de tillgängliga menyvalens siffror.\n");
                        break;
                }
            }
        }

        /*
         * Kontrollera vilken åldersgrupp en person tillhör
         * och visa aktuellt biljettpris för åldersgruppen.
         * Tar emot indata från stdin och skriver ut till stdout.
         */
        private static void CheckAgeAndShowPrice()
        {
            Console.WriteLine("Visa aktuellt biljettpris beroende på en persons ålder");

            int age;
            while (true)
            {
                Console.Write("Ange ålder: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out age) && age >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Felaktig ålder. Du måste ange en korrekt ålder med siffror.");
                }
            }

            if (age < 5)
            {
                Console.WriteLine("Småbarnspris: 0 kr");
            }
            else if (age < 20)
            {
                Console.WriteLine("Ungdomspris: 80 kr");
            }
            else if (age > 100)
            {
                Console.WriteLine("Äldre pensionärs-pris: 0 kr");
            }
            else if (age > 64)
            {
                Console.WriteLine("Pensionärspris: 90 kr");
            }
            else
            {
                Console.WriteLine("Standardpris: 120 kr");
            }

            WaitForInput();
        }

        /* 
         * Beräkna totalpriset för ett sällskap biobesökare.
         * Frågar efter antal personer och alla personernas åldrar för att avgöra priset.
         * Tar emot indata från stdin och skriver ut till stdout.
         */
        private static void CalculatePriceForGroup()
        {
            Console.WriteLine("Beräkna totalpriset för biljetter för ett sällskap");

            int antal;
            while (true)
            {
                Console.Write("Ange antalet personer i sällskapet: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out antal))
                {
                    if (antal > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt antal. Antalet måste vara minst 1.");
                    }
                }
                else
                {
                    Console.WriteLine("Felaktigt antal. Du måste ange antalet med siffror.");
                }
            }

            int kostnad = 0;
            for (int i = 1; i <= antal; i++)
            {
                int age;
                while (true)
                {
                    Console.Write("Ange ålder på person " + i + ": ");
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out age) && age >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Felaktig ålder. Du måste ange en korrekt ålder med siffror.");
                    }
                }

                kostnad +=
                    age < 5 || age > 100 ? 0 :
                    age < 20 ? 80 :
                    age > 64 ? 90 :
                    120;
            }

            Console.WriteLine("Antal personer: " + antal);
            Console.WriteLine($"Totalkostnad: {kostnad:C}");

            WaitForInput();
        }

        /*
         * Tar emot en textsträng och skriver ut den tio gånger.
         * Tar emot indata från stdin och skriver ut till stdout.
         */
        private static void RepeatTenTimes()
        {
            Console.WriteLine("Upprepa en text tio gånger");
            Console.Write("Ange en godtycklig text: ");
            string indata = Console.ReadLine() ?? "";
            for (int i = 1; i <= 10; i++)
            {
                if (i > 1)
                {
                    Console.Write(", ");
                }
                Console.Write(i + ". " + indata);
            }
            Console.WriteLine();

            WaitForInput();
        }

        /*
         * Tar emot en textsträng och skriver ut det tredje ordet.
         * Tar emot indata från stdin och skriver ut till stdout.
         */
        private static void SelectThirdWord()
        {
            Console.WriteLine("Välj ut det tredje ordet från en text");
            while (true)
            {
                Console.Write("Ange en mening med minst tre ord: ");
                string indata = Console.ReadLine() ?? "";
                var s = indata.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (s.Length >= 3)
                {
                    Console.WriteLine(s[2]);
                    break;
                }
                else
                {
                    Console.WriteLine("Felaktig mening. Meningen måste innehålla minst tre ord.");
                }
            }

            WaitForInput();
        }

        /*
         * Väntar tills användaren tryckt på enter.
         * Metoden används för att vänta innan huvudmenyn visas igen,
         * så att användaren lättare ska kunna läsa den föregående utdatan.
         */
        private static void WaitForInput()
        {
            Console.WriteLine("\nTryck enter för att återgå till huvudmenyn.");
            Console.ReadLine();
        }
    }
}
