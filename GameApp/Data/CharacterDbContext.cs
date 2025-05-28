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
    }
}