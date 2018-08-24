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

        public virtual DbSet<GachaHistory> GachaHistory { get; set; }

        public virtual DbSet<GachaHistoryDetail> GachaHistoryDetail { get; set; }

        public virtual DbSet<Reminder> Reminder { get; set; }

        public virtual DbSet<ReminderRecipient> ReminderRecipient { get; set; }

        public virtual DbSet<Shipfu> Shipfu { get; set; }

        public virtual DbSet<ShipfuRarity> ShipfuRarity { get; set; }
    }
}