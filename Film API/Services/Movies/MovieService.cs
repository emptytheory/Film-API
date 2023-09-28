using Film_API.Data;
using Film_API.Data.Entities;
using Film_API.Data.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Film_API.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly FilmDbContext _context;

        public MovieService(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Movie? movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), id);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync<Movie>();
        }

        /// <summary>
        /// Gets a movie by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
                throw new EntityNotFoundException(nameof(Movie), id);

            return movie;
        }

        public async Task<Movie> UpdateAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            
            if (_context.SaveChanges() <= 0)
                throw new NoEffectUpdateException(nameof(Movie), movie.Id);

            return movie;
        }

        public Movie UpdateCharacters(int[] characterIds, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<Character[]> GetCharacterArrayFromIdArray(int[] idArray) 
        {
            return await _context.Characters.Where(c => idArray.Contains(c.Id)).ToArrayAsync();
        }
    }
}
