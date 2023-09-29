using Film_API.Data.Entities;

namespace Film_API.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Task<Movie> UpdateCharactersAsync(int[] characterIds, int movieId);

        Task<IEnumerable<Movie>> GetMoviesByFranchiseIdAsync(int franchiseId);
        // Task<Character[]> GetCharacterArrayFromIdArray(int[] idArray);
    }
}
