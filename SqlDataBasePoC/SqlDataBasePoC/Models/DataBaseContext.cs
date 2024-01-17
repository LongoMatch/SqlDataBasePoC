using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqlDataBasePoC.Models.Configurations;

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
        //
        // Generate the pre-compiled models        
        // dotnet ef dbcontext optimize -o CompiledModels -n SqlDataBasePoC.Models


        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public string DbPath { get; }

        public const string APP_HOST_DIRECTORY = "Library";

        public const string APP_ROOT_DIRECTORY = "SqlDataBasePoC";

        public DataBaseContext() : base()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), APP_HOST_DIRECTORY, APP_ROOT_DIRECTORY);
            CreateDirectory(folder);
            DbPath = Path.Join(folder, "teams.db");

            SQLitePCL.Batteries_V2.Init();

            // In a new installation, apply all migrations, including the initial migration that creates the database.
            // The first time the app starts after an update, the new migrations (those that have not yet been executed) will be run.
            // It's not necessary to call Database.EnsureCreated because Migrate will create the database when running the first migration if it is not created, and it also causes exceptions on iOS.
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .LogTo(LogDataBase, LogLevel.Warning | LogLevel.Error|LogLevel.Critical)
                .UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }
        }

        // Use this method to send DataBase erros to diagnostic services (like AppCenter)
        private void LogDataBase(string message)
            => System.Diagnostics.Debug.WriteLine(message);
    }
}