using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class RandomBotDbContext : DbContext
    {
        public RandomBotDbContext(DbContextOptions<RandomBotDbContext> options) : base(options) { }

        public virtual DbSet<Doll> Doll { get; set; }
        public virtual DbSet<DollType> DollType { get; set; }
        public virtual DbSet<GachaHistory> GachaHistory { get; set; }
        public virtual DbSet<GachaHistoryDetail> GachaHistoryDetail { get; set; }
        public virtual DbSet<HeavyOrdnanceCorp> HeavyOrdnanceCorp { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }
        public virtual DbSet<ReminderRecipient> ReminderRecipient { get; set; }
        public virtual DbSet<Shipfu> Shipfu { get; set; }
        public virtual DbSet<ShipfuRarity> ShipfuRarity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doll>(entity =>
            {
                entity.Property(e => e.DollId).ValueGeneratedNever();

                entity.Property(e => e.Accuracy).IsUnicode(false);

                entity.Property(e => e.Armor).IsUnicode(false);

                entity.Property(e => e.ClipSize).IsUnicode(false);

                entity.Property(e => e.Damage).IsUnicode(false);

                entity.Property(e => e.DollTypeCode).IsUnicode(false);

                entity.Property(e => e.Evasion).IsUnicode(false);

                entity.Property(e => e.Hp).IsUnicode(false);

                entity.Property(e => e.Rof).IsUnicode(false);

                entity.Property(e => e.Skill1).IsUnicode(false);

                entity.Property(e => e.Skill2).IsUnicode(false);

                entity.Property(e => e.TileBonusLocation).IsUnicode(false);

                entity.Property(e => e.TileEffect).IsUnicode(false);

                entity.Property(e => e.TileModBonusLocation).IsUnicode(false);

                entity.HasOne(d => d.DollTypeCodeNavigation)
                    .WithMany(p => p.Doll)
                    .HasForeignKey(d => d.DollTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doll_DollType");
            });

            modelBuilder.Entity<DollType>(entity =>
            {
                entity.Property(e => e.DollTypeCode).IsUnicode(false);

                entity.Property(e => e.DollTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<GachaHistory>(entity =>
            {
                entity.Property(e => e.UserId).IsUnicode(false);
            });

            modelBuilder.Entity<GachaHistoryDetail>(entity =>
            {
                entity.Property(e => e.UserId).IsUnicode(false);

                entity.HasOne(d => d.Shipfu)
                    .WithMany(p => p.GachaHistoryDetail)
                    .HasForeignKey(d => d.ShipfuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipfu_ShipfuId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GachaHistoryDetail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GachaHistoryDetail_UserId");
            });

            modelBuilder.Entity<HeavyOrdnanceCorp>(entity =>
            {
                entity.HasKey(e => e.HocId)
                    .HasName("PK__HeavyOrd__2BB09C329122BDC8");

                entity.Property(e => e.HocId).ValueGeneratedNever();

                entity.Property(e => e.Chip).IsUnicode(false);

                entity.Property(e => e.Lethality).IsUnicode(false);

                entity.Property(e => e.NormalAttack).IsUnicode(false);

                entity.Property(e => e.Pierce).IsUnicode(false);

                entity.Property(e => e.Precision).IsUnicode(false);

                entity.Property(e => e.Range).IsUnicode(false);

                entity.Property(e => e.Reload).IsUnicode(false);

                entity.Property(e => e.Skill1).IsUnicode(false);

                entity.Property(e => e.Skill2).IsUnicode(false);

                entity.Property(e => e.Skill3).IsUnicode(false);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.Property(e => e.ReminderId).ValueGeneratedNever();

                entity.Property(e => e.ReminderMessage).IsUnicode(false);

                entity.HasOne(d => d.ReminderRecipient)
                    .WithMany(p => p.Reminder)
                    .HasForeignKey(d => d.ReminderRecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReminderRecipient");
            });

            modelBuilder.Entity<ReminderRecipient>(entity =>
            {
                entity.Property(e => e.ChannelId).IsUnicode(false);

                entity.Property(e => e.GuildId).IsUnicode(false);
            });

            modelBuilder.Entity<Shipfu>(entity =>
            {
                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShipfuImgUrl).IsUnicode(false);

                entity.Property(e => e.ShipfuName).IsUnicode(false);

                entity.HasOne(d => d.ShipfuRarity)
                    .WithMany(p => p.Shipfu)
                    .HasForeignKey(d => d.ShipfuRarityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipfu_ShipfuRarity");
            });

            modelBuilder.Entity<ShipfuRarity>(entity =>
            {
                entity.Property(e => e.ShipfuRarityName).IsUnicode(false);
            });
        }
    }
}
