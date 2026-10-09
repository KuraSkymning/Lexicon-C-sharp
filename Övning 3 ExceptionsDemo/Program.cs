namespace ExceptionsDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            {
                Console.WriteLine("=== Start av programmet ===");

                // Exempel 1: try-catch-finally
                try
                {
                    Console.WriteLine("Försöker läsa fil och räkna...");
                    var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
                    // För att kasta ArgumentNullException:
                    //var path = Path.Combine(AppContext.BaseDirectory, null);
                    var result = ProcessFile(path);
                    // För att kasta ArgumentException:
                    //var result = ProcessFile("");

                    Console.WriteLine($"\nResultat: {result}");
                }
                // FileNotFoundException kan kastas av ProcessFile()
                catch (FileNotFoundException ex)
                {
                    // Specifikt fel om filen inte finns
                    Console.WriteLine($"Filen hittades inte: {ex.Message}");
                }
                // FormatException kan kastas av ProcessFile()
                catch (FormatException ex)
                {
                    // Specifikt fel om texten inte kan tolkas som tal
                    Console.WriteLine($"Formatfel: {ex.Message}");
                }
                // DivideByZeroException kan kastas av ProcessFile()
                catch (DivideByZeroException ex)
                {
                    // Specifikt fel om nolldivision
                    Console.WriteLine($"Kan inte dividera med noll: {ex.Message}");
                }
                // OverflowException kan kastas av ProcessFile()
                catch (OverflowException)
                {
                    Console.WriteLine("Overflow på grund av för stort tal.");
                }
                // InvalidOperationException kan kastas av ProcessFile()
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // OutOfMemoryException kan kastas av ProcessFile()
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"Slut på minne: {ex.Message}");
                }
                // IOException kan kastas av Console.WriteLine() och ProcessFile()
                catch (IOException ex)
                {
                    Console.WriteLine($"IO-fel: {ex.Message}");
                }
                // ArgumentException kan kastas av ProcessFile()
                // (och Path.Combine(), men inte i den här koden)
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // ArgumentNullException kan kastas av Path.Combine(), men inte i den här koden
                catch (Exception ex)
                {
                    // Fallback för alla övriga obekanta fel
                    Console.WriteLine($"Okänt fel: {ex.Message}");
                }
                finally
                {
                    // Körs ALLTID, även om det blev undantag
                    Console.WriteLine("Cleanup: Logging avslutat anrop.");
                }

                Console.WriteLine("Programmet avslutas normalt.");
            }

            // Exempel på metod som själv kastar ett undantag (throw)
            static double ProcessFile(string fileName)
            {
                // Om filnamnet är tomt: logiskt fel vi vill signalera
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentException("Filnamn får inte vara tomt eller null.", nameof(fileName));
                }

                StreamReader? reader = null;
                try
                {
                    reader = new StreamReader(fileName);

                    string? line = reader.ReadLine();
                    if (line == null)
                        throw new InvalidOperationException("Filen är tom.");

                    // Försöker omvandla text till tal
                    int number = int.Parse(line); // Kan ge FormatException

                    // Division: kan ge DivideByZeroException
                    // Castar uttrycket från decimal för att verkligen orsaka DivideByZeroException
                    // istället för att returnera Double.PositiveInfinity.
                    return (double)(100.0M / number);
                    //return 100.0 / number;
                }
                // FileNotFoundException kan kastas av new StreamReader()
                catch (FileNotFoundException)
                {
                    throw;
                }
                // DivideByZeroException kan kastas vid användning av operatorn /
                catch (DivideByZeroException)
                {
                    throw;
                }
                // InvalidOperationException kastas explicit i try-blocket
                catch (InvalidOperationException)
                {
                    throw;
                }
                // IOException kan kastas av StreamReader.ReadLine()
                catch (IOException)
                {
                    throw;
                }
                // OutOfMemoryException kan kastas av StreamReader.ReadLine()
                catch (OutOfMemoryException)
                {
                    throw;
                }
                // OverflowException kan kastas av int.Parse()
                catch (OverflowException)
                {
                    throw;
                }
                // FormatException kan kastas av int.Parse()
                catch (FormatException ex)
                {
                    // Vi kan logga eller omformulera felet
                    //Console.WriteLine($"Formatfel i ProcessFile: {ex.Message}");
                    // Vi kan välja att låta metoden "kasta upp" felet
                    throw; // När du i `catch` bara vill logga/analysera,
                    // men låta anroparen (t.ex. en högre nivå i applikationen)
                    // bestämma hur man ska återhämta sig. 
                }
                // ArgumentException, ArgumentNullException och DirectoryNotFoundException
                // kan kastas av new StreamReader(), men inte i den här koden.
                catch (Exception ex)
                {
                    // Om vi vill ge en mer meningsfull feltyp till anroparen
                    throw new InvalidOperationException(
                    "Det gick inte att processa filen.",
                    ex); // InnerException = ursprunglig fel
                }
                finally
                {
                    // Garanterad stängning av resurs
                    reader?.Close();    // Kan inte kasta något exception
                    Console.WriteLine("finally i ProcessFile: StreamReader stängd.");
                }
            }
        }
    }
}

