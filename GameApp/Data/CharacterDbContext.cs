using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameApp.Models;

namespace GameApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gamecharacters.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many Character <-> Equipment через таблицу CharacterEquipment
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Equipment)
                .WithMany(e => e.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterEquipment",
                    j => j
                        .HasOne<Equipment>()
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Character>()
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade));

            // One-to-many Character -> Skills
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Character)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many Class -> Characters
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Class)
                .WithMany(cl => cl.Characters)
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many Class -> Skills (nullable)
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Class)
                .WithMany(cl => cl.Skills)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.SetNull);

            // One-to-many Class -> Equipment (nullable)
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Class)
                .WithMany(cl => cl.Equipment)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}