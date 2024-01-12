using System;
namespace SqlDataBasePoC.Models
{
    public class Player
    {
        public int Id { get; set; }

        // Mandatory field
        public string Name { get; set; } = null!;

        // Optional field
        public string? Position { get; set; }

        // Required foreign key
        public int TeamId { get; set; }

        // Required reference navigation to principal
        public Team Team { get; set; } = null!;

        public Player()
        {
        }
    }
}