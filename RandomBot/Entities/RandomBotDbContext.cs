using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class RandomBotDbContext : DbContext
    {
        public RandomBotDbContext(DbContextOptions<RandomBotDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Shipfu> Shipfu { get; set; }

        public virtual DbSet<ShipfuRarity> ShipfuRarity { get; set; }
    }
}