using System;
using System.Collections.Generic;
using System.Numerics;

namespace SqlDataBasePoC.Models
{
    public class Team
    {
        // Primary key
        public int Id { get; set; }

        // Mandatory field
        public string Name { get; set; } = null!;

        // Optional field
        public string? Country { get; set; }

        // Collection Navigation
        public List<Player> Players { get; } = new List<Player>();

        public Team()
        {
        }
    }
}