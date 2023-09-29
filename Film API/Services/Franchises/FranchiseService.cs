using Film_API.Data;
using Film_API.Data.Entities;
using Film_API.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Film_API.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly FilmDbContext _context;

        public FranchiseService(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<Franchise> AddAsync(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task DeleteByIdAsync(int id)
        {
            Franchise? franchise = await _context.Franchises.FindAsync(id);
            
            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), id);

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            Franchise? franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), id);

            return franchise;
        }

        public async Task<Franchise> UpdateAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() <= 0)
                throw new NoRowsAffectedException(nameof(Franchise), franchise.Id);

            return franchise;
        }

        public async Task<Franchise> UpdateMoviesAsync(int[] movieIds, int franchiseId)
        {
            Franchise? franchise = await _context.Franchises.FindAsync(franchiseId);

            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), franchiseId);

            HashSet<Movie> movies = new(await _context.Movies.Where(m => movieIds.Contains(m.Id)).ToArrayAsync());

            franchise.Movies = movies;

            _context.Entry(franchise).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() <= 0)
                throw new NoRowsAffectedException(nameof(Franchise), franchise.Id);

            return franchise;
        }
    }
}
