﻿using Microsoft.EntityFrameworkCore;

namespace ComplimentSlots
{
    public class ComplimentDbContext : DbContext
    {
        public DbSet<Adjective> Adjectives { get; set; }
        public DbSet<AdjectiveVerb> AdjectiveVerbs { get; set; }
        public DbSet<Noun> Nouns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Change this connection string as per your database setup
            optionsBuilder.UseSqlite("Data Source=complimentSlots.db");
        }
    }
}
