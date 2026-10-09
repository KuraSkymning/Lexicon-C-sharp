using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace TestProject
{
    public class Tester
    {
        // Följande exceptions testas:
        // - DivideByZeroException
        // - FileNotFoundException
        // - FormatException
        // - InvalidOperationException
        // - OverflowException

        // Följande exceptions kan testas genom att ändra koden i Program.cs:
        // - ArgumentException
        // - ArgumentNullException

        // Följande exceptions är svåra att testa:
        // - IOException
        // - OutOfMemoryException

        [Fact]
        public void TestInteger3()
        {
            string input = "3";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: 33,333333333333336",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger5()
        {
            string input = "5";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: 20",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger10()
        {
            string input = "10";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: 10",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger100()
        {
            string input = "100";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: 1",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger101()
        {
            string input = "101";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: 0,9900990099009901",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestIntegerMinus3()
        {
            string input = "-3";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: -33,333333333333336",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestIntegerMinus10()
        {
            string input = "-10";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                "Resultat: -10",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger0()
        {
            string input = "0";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                // DivideByZeroException
                "Kan inte dividera med noll: Attempted to divide by zero.",
                //"Resultat: ∞",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestIntegerMinus0()
        {
            string input = "-0";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                // DivideByZeroException
                "Kan inte dividera med noll: Attempted to divide by zero.",
                //"Resultat: ∞",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInteger999999999999()
        {
            string input = "999999999999";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                // OverflowException
                "Overflow på grund av för stort tal.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLetterA()
        {
            string input = "A";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                //"Formatfel i ProcessFile: The input string 'A' was not in a correct format.",
                "finally i ProcessFile: StreamReader stängd.",
                // FormatException
                "Formatfel: The input string 'A' was not in a correct format.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDecimal10Point3()
        {
            string input = "10.3";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                //"Formatfel i ProcessFile: The input string '10.3' was not in a correct format.",
                "finally i ProcessFile: StreamReader stängd.",
                // FormatException
                "Formatfel: The input string '10.3' was not in a correct format.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDecimal0Point0()
        {
            string input = "0.0";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                //"Formatfel i ProcessFile: The input string '0.0' was not in a correct format.",
                "finally i ProcessFile: StreamReader stängd.",
                // FormatException
                "Formatfel: The input string '0.0' was not in a correct format.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSpace()
        {
            string input = " ";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                //"Formatfel i ProcessFile: The input string ' ' was not in a correct format.",
                "finally i ProcessFile: StreamReader stängd.",
                // FormatException
                "Formatfel: The input string ' ' was not in a correct format.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestEmpty()
        {
            string input = "";
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                // InvalidOperationException
                "Filen är tom.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = ModifyFileAndRun(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNoFile()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
            string[] expected =
                ["=== Start av programmet ===",
                "Försöker läsa fil och räkna...",
                "finally i ProcessFile: StreamReader stängd.",
                // FileNotFoundException
                "Filen hittades inte: Could not find file '" + path + "'.",
                "Cleanup: Logging avslutat anrop.",
                "Programmet avslutas normalt."];

            string[] actual = RemoveFileAndRun();
            Assert.Equal(expected, actual);
        }

        // Skriver input till filen numbers.txt,
        // och kör därefter ExceptionsDemo.Program.Main() och returnerar dess utdata.
        // Tomma rader i utdatan ignoreras.
        static private string[] ModifyFileAndRun(string input)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
            File.WriteAllText(path, input);

            using StringWriter sw = new();
            Console.SetOut(sw);
            ExceptionsDemo.Program.Main([]);

            return sw.ToString().Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        // Tar bort filen numbers.txt,
        // och kör därefter ExceptionsDemo.Program.Main() och returnerar dess utdata.
        // Tomma rader i utdatan ignoreras.
        static private string[] RemoveFileAndRun()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
            File.Delete(path);

            using StringWriter sw = new();
            Console.SetOut(sw);
            ExceptionsDemo.Program.Main([]);

            return sw.ToString().Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
    }
}
