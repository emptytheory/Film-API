using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Film_API.Data.Entities
{
    [Table(nameof(Movie))]

    public class Movie
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50)]
        public string Director { get; set; }


        public string? Picture { get; set; }


        public string? Trailer { get; set; }

        public int? FranchiseId { get; set; }

        // Navigation
        public Franchise? Franchise { get; set; }
        public HashSet<Character> Characters { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Movie movie &&
                   Id == movie.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
