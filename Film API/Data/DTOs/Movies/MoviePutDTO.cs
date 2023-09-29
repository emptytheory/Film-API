namespace Film_API.Data.DTOs.Movies
{
    public class MoviePutDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string? Picture { get; set; }
        public string? Trailer { get; set; }
        // public int? FranchiseId { get; set; } // Set in a different endpoint?
    }
}
