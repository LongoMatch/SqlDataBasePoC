using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SqlDataBasePoC.Models
{
    public class DataBaseContext : DbContext
    {
        // Terminal commands
        // dotnet tool install --global dotnet-ef
        // dotnet add package Microsoft.EntityFrameworkCore.Design
        // dotnet add package Microsoft.EntityFrameworkCore.Sqlite
        // dotnet ef migrations add InitialCreate
        // dotnet ef database update

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public string DbPath { get; }

        public const string APP_HOST_DIRECTORY = "Library";

        public const string APP_ROOT_DIRECTORY = "SqlDataBasePoC";

        public DataBaseContext()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), APP_HOST_DIRECTORY, APP_ROOT_DIRECTORY);
            CreateDirectory(folder);
            DbPath = Path.Join(folder, "teams.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<Team>().Property(t => t.Country).IsRequired(false);

            // This 1:N relationship is created by convention (adding the fields Players, TeamId and Team)
            // However its also possible to define manually.
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .IsRequired();

            modelBuilder.Entity<Player>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Player>().Property(p => p.Position).IsRequired(false);
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }
        }
    }
}