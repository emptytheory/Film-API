using Film_API.Data.Entities;

namespace Film_API.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Movie UpdateCharacters(int[] characterIds, int movieId);
    }
}
