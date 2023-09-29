using Film_API.Data;
using Film_API.Data.Entities;
using Film_API.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Film_API.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly FilmDbContext _context;

        public CharacterService( FilmDbContext context ) 
        {
            _context = context;
        }

        public async Task<Character> AddAsync(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Character? character = await _context.Characters.FindAsync(id);
            if (character is null)
                throw new EntityNotFoundException(nameof(Character), id);

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character is null)
                throw new EntityNotFoundException(nameof(Character), id);

            return character;
        }

        public async Task<IEnumerable<Character>> GetCharactersByFranchiseIdAsync(int franchiseId)
        {
            Franchise? franchise = await _context.Franchises
                .Include(f => f.Movies)
                .ThenInclude(m => m.Characters)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);

            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), franchiseId);

            var characters = franchise.Movies.SelectMany(m => m.Characters).Distinct();

            return characters;
        }

        public async Task<IEnumerable<Character>> GetCharactersByMovieIdAsync(int movieId)
        {
            Movie? movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie is null)
                throw new EntityNotFoundException(nameof(Movie), movieId);

            return movie.Characters;
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() <= 0)
                throw new NoRowsAffectedException(nameof(Character), character.Id);

            return character;
        }

        public async Task<Character> UpdateMoviesAsync(int[] movieIds, int characterId)
        {
            Character? character = await _context.Characters.FindAsync(characterId);

            if (character is null)
                throw new EntityNotFoundException(nameof(Character), characterId);

            HashSet<Movie> movies = new(await _context.Movies.Where(m => movieIds.Contains(m.Id)).ToArrayAsync());

            character.Movies = movies;

            _context.Entry(character).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() <= 0)
                throw new NoRowsAffectedException(nameof(Character), character.Id);

            return character;
        }
    }
}
