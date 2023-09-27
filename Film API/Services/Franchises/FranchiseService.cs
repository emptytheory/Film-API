using Film_API.Data;
using Film_API.Data.Entities;

namespace Film_API.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly FilmDbContext _context;

        public FranchiseService(FilmDbContext context)
        {
            _context = context;
        }

        public Task<Franchise> AddAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Franchise>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> UpdateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Franchise UpdateMovies(int[] movieIds, int franchiseId)
        {
            throw new NotImplementedException();
        }
    }
}
