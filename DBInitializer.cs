using System.Linq;

namespace ComplimentSlots
{
    public static class DbInitializer
    {
        public static void SeedTables(ComplimentDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Adjectives.Any())
            {
                PopulateAdjectives(context);
            }

            if (!context.AdjectiveVerbs.Any())
            {
                PopulateAdjectiveVerbs(context);
            }

            if (!context.Nouns.Any())
            {
                PopulateNouns(context);
            }

            void PopulateAdjectives(ComplimentDbContext ctx)
            {
                string[] adjectives = {
                    "Beautiful", "Brilliant", "Charming", "Delightful", "Elegant",
                    "Fantastic", "Graceful", "Handsome", "Inspiring", "Joyful"
                };

                foreach (var word in adjectives)
                {
                    ctx.Adjectives.Add(new Adjective { Word = word });
                }

                ctx.SaveChanges();
            }

            void PopulateAdjectiveVerbs(ComplimentDbContext ctx)
            {
                string[] adjectiveVerbs = {
                    "Courageous", "Enchanting", "Fascinating", "Generous", "Harmonious",
                    "Inspiring", "Majestic", "Noble", "Resilient", "Serene"
                };

                foreach (var word in adjectiveVerbs)
                {
                    ctx.AdjectiveVerbs.Add(new AdjectiveVerb { Word = word });
                }

                ctx.SaveChanges();
            }

            void PopulateNouns(ComplimentDbContext ctx)
            {
                string[] nouns = {
                    "Fella", "Person", "Leader", "Friend", "Companion",
                    "Partner", "Individual", "Soul", "Gem", "Treasure"
                };

                foreach (var word in nouns)
                {
                    ctx.Nouns.Add(new Noun { Word = word });
                }

                ctx.SaveChanges();
            }
        }
    }
}
