using System;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using SqlDataBasePoC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SqlDataBasePoC.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
    }

    private async void OnReadDataBase(object? sender, RoutedEventArgs e)
    {
        await InitializeDataAsync();
        using (var db = new DataBaseContext())
        {
            foreach (var team in db.Teams.Include(t => t.Players))
            {
                System.Diagnostics.Debug.WriteLine($"Team - Id: {team.Id} Name: {team.Name} Country: {team.Country} Players total: {team.Players.Count}");
                foreach (var player in team.Players)
                {
                    System.Diagnostics.Debug.WriteLine($"Player - Id: {player.Id} Name: {player.Name} Position: {player.Position} TeamId: {player.TeamId} Team: {player.Team.Name}");
                }
            }
        }
    }
    private async Task InitializeDataAsync()
    {
        using (var db = new DataBaseContext())
        {
/*
            Calling BeginTransactionAsync before executing changes in the database and CommitTransactionAsync
            after saving the changes in the database ensures that all operations will be performed at once
            and that if a failure occurs during execution, no changes will be applied, preventing the creation
            of an inconsistent state in the database. This reduces performance but is safer.
*/
            await db.Database.BeginTransactionAsync();

            var bcn = new Team() { Name = "FC Barcelona", Country = "Spain" };
            var pbcn1 = new Player() { Name = "Marc Andre Ter Stegen", Position = "GK" };
            bcn.Players.Add(pbcn1);
            await db.AddAsync(bcn);
            await db.SaveChangesAsync();

            var rm = new Team() { Name = "Real Madrid CF", Country = "Spain" };
            await db.AddAsync(rm);
            await db.SaveChangesAsync();

            await db.Database.CommitTransactionAsync();
        }
    }
}