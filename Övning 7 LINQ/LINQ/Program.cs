using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    public class Student
    {
        public int Id { get; set; }
        public string Namn { get; set; } = "";
        public int Alder { get; set; }
        public string Klass { get; set; } = "";
        public int Poang { get; set; }

        public override string ToString()
        {
            return $"{Id} {Namn}, {Alder} år, klass {Klass}, {Poang} poäng";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Exempeldata
            List<Student> studenter = [
                new Student { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 },
                new Student { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 },
                new Student { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 },
                new Student { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 },
                new Student { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 },
                new Student { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 },
                new Student { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 },
                new Student { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 },
            ];
            Console.WriteLine("LINQ-ÖVNINGAR MED STUDENTER\n");

            // Where

            // • Hämta alla studenter som är äldre än 20.
            Console.WriteLine("Hämta alla studenter som är äldre än 20.\n");

            Console.WriteLine("Med metodsyntax:");
            var r = studenter.Where(s => s.Alder > 20);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            r = from s in studenter
                where s.Alder > 20
                select s;
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            // • Hämta alla studenter som har mer än 80 poäng.
            Console.WriteLine("Hämta alla studenter som har mer än 80 poäng.\n");

            Console.WriteLine("Med metodsyntax:");
            r = studenter.Where(s => s.Poang > 80);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            r = from s in studenter
                where s.Poang > 80
                select s;
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            // Select

            // • Visa bara namnen på alla studenter.
            Console.WriteLine("Visa bara namnen på alla studenter.\n");

            Console.WriteLine("Med metodsyntax:");
            var s1 = studenter.Select(s => s.Namn);
            Console.WriteLine(string.Join("\n", s1));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            s1 = from s in studenter
                 select s.Namn;
            Console.WriteLine(string.Join("\n", s1));

            Console.WriteLine();

            // • Visa namn och poäng i en ny anonym typ.
            Console.WriteLine("Visa namn och poäng i en ny anonym typ.\n");

            Console.WriteLine("Med metodsyntax:");
            var a1 = studenter.Select(s => new { s.Namn, s.Poang });
            foreach (var s in a1)
            {
                Console.WriteLine($"{s.Namn}, {s.Poang}");
            }

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            a1 = from s in studenter
                 select new { s.Namn, s.Poang };
            foreach (var s in a1)
            {
                Console.WriteLine($"{s.Namn}, {s.Poang}");
            }

            Console.WriteLine();

            // OrderBy

            // • Sortera studenter efter ålder.
            Console.WriteLine("Sortera studenter efter ålder.\n");

            Console.WriteLine("Med metodsyntax:");
            r = studenter.OrderBy(s => s.Alder);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            r = from s in studenter
                orderby s.Alder
                select s;
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            // • Sortera studenter efter poäng, högst först.
            Console.WriteLine("Sortera studenter efter poäng, högst först.\n");

            Console.WriteLine("Med metodsyntax:");
            r = studenter.OrderByDescending(s => s.Poang);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            r = from s in studenter
                orderby s.Poang descending
                select s;
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            // Count

            // • Räkna hur många studenter som finns.
            Console.WriteLine("Räkna hur många studenter som finns.\n");

            Console.WriteLine("Med metodsyntax:");
            int n = studenter.Count();
            Console.WriteLine(n);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n = (from s in studenter
                 select s).Count();
            Console.WriteLine(n);

            Console.WriteLine();

            // • Räkna hur många som går i klass A.
            Console.WriteLine("Räkna hur många som går i klass A.\n");

            Console.WriteLine("Med metodsyntax:");
            n = studenter.Where(s => s.Klass == "A").Count();
            Console.WriteLine(n);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n = (from s in studenter
                 where s.Klass == "A"
                 select s).Count();
            Console.WriteLine(n);

            Console.WriteLine();

            // Take

            // • Ta ut de tre första studenterna.
            Console.WriteLine("Ta ut de tre första studenterna.\n");

            Console.WriteLine("Med metodsyntax:");
            r = studenter.Take(3);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            r = (from s in studenter
                 select s).Take(3);
            Console.WriteLine(string.Join("\n", r));

            Console.WriteLine();

            // • Ta ut de två bästa poängen efter sortering.
            Console.WriteLine("Ta ut de två bästa poängen efter sortering.\n");

            Console.WriteLine("Med metodsyntax:");
            var n1 = studenter.Select(s => s.Poang).OrderDescending().Take(2);
            foreach (int j in n1)
            {
                Console.WriteLine(j);
            }

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n1 = (from s in studenter
                  orderby s.Poang descending
                  select s.Poang).Take(2);
            foreach (int j in n1)
            {
                Console.WriteLine(j);
            }

            Console.WriteLine();

            // Distinct

            // • Visa alla unika klassnamn.
            Console.WriteLine("Visa alla unika klassnamn.\n");

            Console.WriteLine("Med metodsyntax:");
            s1 = studenter.Select(s => s.Klass).Distinct();
            Console.WriteLine(string.Join("\n", s1));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            s1 = (from s in studenter
                  select s.Klass).Distinct();
            Console.WriteLine(string.Join("\n", s1));

            Console.WriteLine();

            // • Visa alla unika åldrar.
            Console.WriteLine("Visa alla unika åldrar.\n");

            Console.WriteLine("Med metodsyntax:");
            n1 = studenter.Select(s => s.Alder).Distinct();
            Console.WriteLine(string.Join("\n", n1));

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n1 = (from s in studenter
                  select s.Alder).Distinct();
            Console.WriteLine(string.Join("\n", n1));

            Console.WriteLine();

            // Any / All

            // • Finns det någon student med 100 poäng?
            Console.WriteLine("Finns det någon student med 100 poäng?\n");

            Console.WriteLine("Med metodsyntax:");
            var b = studenter.Any(s => s.Poang == 100);
            Console.WriteLine(b ? "Ja" : "Nej");

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            b = (from s in studenter
                 where s.Alder == 100
                 select s).Any();
            Console.WriteLine(b ? "Ja" : "Nej");

            Console.WriteLine();

            // • Har alla studenter minst 50 poäng?
            Console.WriteLine("Har alla studenter minst 50 poäng?\n");

            Console.WriteLine("Med metodsyntax:");
            b = studenter.All(s => s.Poang >= 50);
            Console.WriteLine(b ? "Ja" : "Nej");

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            b = (from s in studenter
                 select s).All(s => s.Poang >= 50);
            Console.WriteLine(b ? "Ja" : "Nej");

            Console.WriteLine();

            // FirstOrDefault / Last / Single

            // • Hämta första studenten i klass B.
            Console.WriteLine("Hämta första studenten i klass B.\n");

            Console.WriteLine("Med metodsyntax:");
            var s2 = studenter.First(s => s.Klass == "B");
            Console.WriteLine(s2);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            s2 = (from s in studenter
                  where s.Klass == "B"
                  select s).First();
            Console.WriteLine(s2);

            Console.WriteLine();

            // • Hämta sista studenten i listan.
            Console.WriteLine("Hämta sista studenten i listan.\n");

            Console.WriteLine("Med metodsyntax:");
            s2 = studenter.Last();
            Console.WriteLine(s2);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            s2 = (from s in studenter
                  select s).Last();
            Console.WriteLine(s2);

            Console.WriteLine();

            // • Hämta studenten med ett visst Id.
            Console.WriteLine("Hämta studenten med ett visst Id.\n");

            Console.WriteLine("Med metodsyntax:");
            s2 = studenter.Single(s => s.Id == 1);
            Console.WriteLine(s2);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            s2 = (from s in studenter
                  where s.Id == 1
                  select s).Single();
            Console.WriteLine(s2);

            Console.WriteLine();

            // GroupBy

            // • Gruppera studenter per klass.
            Console.WriteLine("Gruppera studenter per klass.\n");

            Console.WriteLine("Med metodsyntax:");
            var g1 = studenter.GroupBy(s => s.Klass);
            foreach (var g in g1)
            {
                Console.WriteLine("Klass " + g.Key);
                foreach (var s in g)
                {
                    Console.WriteLine(" " + s);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            g1 = from s in studenter
                 group s by s.Klass into gr
                 select gr;
            foreach (var g in g1)
            {
                Console.WriteLine("Klass " + g.Key);
                foreach (var s in g)
                {
                    Console.WriteLine(" " + s);
                }
            }

            Console.WriteLine();

            // • Skriv ut varje klass och namnen i gruppen.
            Console.WriteLine("Skriv ut varje klass och namnen i gruppen.\n");

            Console.WriteLine("Med metodsyntax:");
            g1 = studenter.GroupBy(s => s.Klass);
            foreach (var g in g1)
            {
                Console.WriteLine("Klass " + g.Key);
                foreach (var s in g)
                {
                    Console.WriteLine(" " + s.Namn);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            g1 = from s in studenter
                 group s by s.Klass into gr
                 select gr;
            foreach (var g in g1)
            {
                Console.WriteLine("Klass " + g.Key);
                foreach (var s in g)
                {
                    Console.WriteLine(" " + s.Namn);
                }
            }
            Console.WriteLine();

            // Sum / Average / Min / Max

            // • Summera alla poäng.
            Console.WriteLine("Summera alla poäng.\n");

            Console.WriteLine("Med metodsyntax:");
            n = studenter.Sum(s => s.Poang);
            Console.WriteLine(n);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n = (from s in studenter
                 select s.Poang).Sum();
            Console.WriteLine(n);

            Console.WriteLine();

            // • Räkna ut medelpoäng.
            Console.WriteLine("Räkna ut medelpoäng.\n");

            Console.WriteLine("Med metodsyntax:");
            var d = studenter.Average(s => s.Poang);
            Console.WriteLine(d);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            d = (from s in studenter
                 select s.Poang).Average();
            Console.WriteLine(d);

            Console.WriteLine();

            // • Hitta lägsta och högsta poäng.
            Console.WriteLine("Hitta lägsta och högsta poäng.\n");

            Console.WriteLine("Med metodsyntax:");
            n = studenter.Min(s => s.Poang);
            Console.WriteLine(n);
            n = studenter.Max(s => s.Poang);
            Console.WriteLine(n);

            Console.WriteLine();

            Console.WriteLine("Med frågesyntax:");
            n = (from s in studenter
                 select s.Poang).Min();
            Console.WriteLine(n);
            n = (from s in studenter
                 select s.Poang).Max();
            Console.WriteLine(n);
        }
    }
}