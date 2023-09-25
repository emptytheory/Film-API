﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Film_API.Data.Entities
{
    [Table(nameof(Franchise))]
    public class Franchise
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation
        public HashSet<Movie> Movies { get; set; }
    }
}
