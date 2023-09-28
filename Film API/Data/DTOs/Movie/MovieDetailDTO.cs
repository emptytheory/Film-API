using System.ComponentModel.DataAnnotations;

namespace Film_API.Data.DTOs.Movie
{
    public record MovieDetailDTO(int Id, 
                              string Title, 
                              string Genre, 
                                 int ReleaseYear, 
                              string Director, 
                             string? Picture, 
                             string? Trailer, 
                                int? FranchiseId
                               int[] Characters);
}
