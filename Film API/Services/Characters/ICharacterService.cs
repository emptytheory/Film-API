using Film_API.Data.Entities;

namespace Film_API.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task<Character> UpdateMoviesAsync(int[] movieIds, int characterId);

        Task<IEnumerable<Character>> GetCharactersByMovieIdAsync(int movieId);

        Task<IEnumerable<Character>> GetCharactersByFranchiseIdAsync(int franchiseId);
    }
}