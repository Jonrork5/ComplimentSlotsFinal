using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComplimentSlots
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to the Day Maker Machine! A.K.A Compliment Slots!");
            Console.WriteLine("Type 'SPIN' and hit enter to get a boost of self-confidence!");
            Console.WriteLine("Type 'EXIT' to close the application.");

            using (var context = new ComplimentDbContext())
            {
                
                DbInitializer.SeedTables(context);

                while (true)
                {
                    Console.Write("Enter command: ");
                    string input = Console.ReadLine()?.Trim().ToUpper();

                    if (input == "SPIN")
                    {
                        var adjective = GetRandomWord(context.Adjectives);
                        var adjectiveVerb = GetRandomWord(context.AdjectiveVerbs);
                        var noun = GetRandomWord(context.Nouns);

                        string compliment = $"{adjective.Word.ToLower()} {adjectiveVerb.Word.ToLower()} {noun.Word.ToLower()}";
                        Console.WriteLine(compliment);

                        LogEvent($"Compliment generated: {compliment}");
                    }
                    else if (input == "EXIT")
                    {
                        Console.WriteLine("Exiting the Day Maker Machine. Have a great day!");
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Please type 'SPIN' to receive a compliment or 'EXIT' to close the application.");
                    }
                }
            }
        }

        static T GetRandomWord<T>(DbSet<T> set) where T : class
        {
            return set.ToList().OrderBy(r => Guid.NewGuid()).FirstOrDefault();
        }

        static void LogEvent(string message)
        {
            string logPath = "complimentSlotsLog.txt";
            using (var writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
