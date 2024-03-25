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
            LogEvent("Application Started");
            Console.WriteLine("Welcome to the Compliment Machine!");
            Console.WriteLine("Type SPIN and hit enter to receive a compliment!");

            using (var context = new ComplimentDbContext())
            {
                context.Database.EnsureCreated();

                // Populate tables with words if necessary
                if (!context.Adjectives.Any())
                {
                    PopulateAdjectives(context);
                    LogEvent("Adjectives table populated.");
                }

                if (!context.AdjectiveVerbs.Any())
                {
                    PopulateAdjectiveVerbs(context);
                    LogEvent("AdjectiveVerbs table populated.");
                }

                if (!context.Nouns.Any())
                {
                    PopulateNouns(context);
                    LogEvent("Nouns table populated.");
                }

                // Main loop to generate compliments
                while (true)
                {
                    Console.Write("Enter SPIN to receive a compliment: ");
                    string input = Console.ReadLine();

                    if (input?.ToUpper() == "SPIN")
                    {
                        // Retrieve random words from each table
                        var adjective = GetRandomWord(context.Adjectives);
                        var adjectiveVerb = GetRandomWord(context.AdjectiveVerbs);
                        var noun = GetRandomWord(context.Nouns);

                        // Combine words into a compliment sentence
                        string compliment = $"{adjective.Word.ToLower()} {adjectiveVerb.Word.ToLower()} {noun.Word.ToLower()}";
                        Console.WriteLine(compliment);

                        LogEvent($"Compliment generated: {compliment}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please type SPIN to receive a compliment.");
                    }
                }
            }
        }

        static void PopulateAdjectives(ComplimentDbContext context)
        {
            string[] adjectives = {
                "Beautiful", "Brilliant", "Charming", "Delightful", "Elegant",
                "Fantastic", "Graceful", "Handsome", "Inspiring", "Joyful"
            };

            foreach (var word in adjectives)
            {
                context.Adjectives.Add(new Adjective { Word = word });
            }

            context.SaveChanges();
        }

        static void PopulateAdjectiveVerbs(ComplimentDbContext context)
        {
            string[] adjectiveVerbs = {
                "Courageous", "Enchanting", "Fascinating", "Generous", "Harmonious",
                "Inspiring", "Majestic", "Noble", "Resilient", "Serene"
            };

            foreach (var word in adjectiveVerbs)
            {
                context.AdjectiveVerbs.Add(new AdjectiveVerb { Word = word });
            }

            context.SaveChanges();
        }

        static void PopulateNouns(ComplimentDbContext context)
        {
            string[] nouns = {
                "Fella", "Person", "Leader", "Friend", "Companion",
                "Partner", "Individual", "Soul", "Gem", "Treasure"
            };

            foreach (var word in nouns)
            {
                context.Nouns.Add(new Noun { Word = word });
            }

            context.SaveChanges();
        }

        static T GetRandomWord<T>(DbSet<T> set) where T : class
        {
            return set.ToList().OrderBy(r => Guid.NewGuid()).FirstOrDefault();
        }

        static void LogEvent(string message)
        {
            string logPath = "complimentSlotsLog.txt";
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
