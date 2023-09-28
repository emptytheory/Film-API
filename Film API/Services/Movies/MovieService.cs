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

        public Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
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
            var movie = await _context.Movies.FindAsync(id);

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
    }
}
