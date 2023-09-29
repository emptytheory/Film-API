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
            return await _context.Movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByFranchiseIdAsync(int franchiseId)
        {
            return await _context.Movies.Where(m => m.FranchiseId == franchiseId).ToListAsync();
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

        public async Task<Movie> UpdateCharactersAsync(int[] characterIds, int movieId)
        {
            Movie? movie = await _context.Movies.FindAsync(movieId);

            if (movie is null)
                throw new EntityNotFoundException(nameof(Movie), movieId);

            HashSet<Character> characters = new(await _context.Characters.Where(c => characterIds.Contains(c.Id)).ToArrayAsync());

            movie.Characters = characters;

            _context.Entry(movie).State = EntityState.Modified;

            if (_context.SaveChanges() <= 0)
                throw new NoEffectUpdateException(nameof(Movie), movie.Id);

            return movie;
        }

        //public async Task<Character[]> GetCharacterArrayFromIdArray(int[] idArray) 
        //{
        //    return await _context.Characters.Where(c => idArray.Contains(c.Id)).ToArrayAsync();
        //}
    }
}
