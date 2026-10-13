using System;
using System.IO;
using System.Globalization;

namespace LabEncapsulationDemo.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string ds = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
            string input =
                "5\n" +
                "Ida Svensson 65 2200\n" +
                "Berit Dahl 57 3333\n" +
                "Bert Lewinsson 27 600\n" +
                $"Anna Hamren 44 666{ds}66\n" +
                $"Jacob Andersson 35 559{ds}4\n" +
                "20";
            string[] expected =
                [$"Ida Svensson tjänar {2200*1.2:C}.",
                $"Berit Dahl tjänar {3333*1.2:C}.",
                $"Bert Lewinsson tjänar {600*1.1:C}.",
                $"Anna Hamren tjänar {666.66*1.2:C}.",
                $"Jacob Andersson tjänar {559.4*1.2:C}."];

            using StringReader sr = new(input);
            using StringWriter sw = new();
            Console.SetIn(sr);
            Console.SetOut(sw);
            StartUp.Main();
            string[] actual = sw.ToString().Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test2()
        {
            string input =
                "5\n" +
                "Anna Ivanov -6 2200\n" +
                "B Dahl 57 3333\n" +
                "Hanna Ma 27 600\n" +
                "Asen Holm 44 200\n" +
                "Mikael Johansson 35 500\n" +
                "20";
            string[] expected =
                ["Ålder kan inte vara noll eller negativ.",
                "Förnamn får inte innehålla färre än 3 tecken.",
                "Efternamn får inte innehålla färre än 3 tecken.",
                $"Lön kan inte vara lägre än {460:C}.",
                $"Mikael Johansson tjänar {500*1.2:C}."];

            using StringReader sr = new(input);
            using StringWriter sw = new();
            Console.SetIn(sr);
            Console.SetOut(sw);
            StartUp.Main();
            string[] actual = sw.ToString().Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, actual);
        }
    }
}
