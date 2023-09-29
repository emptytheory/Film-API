using Film_API.Data.Entities;

namespace Film_API.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task<Franchise> UpdateMoviesAsync(int[] movieIds, int franchiseId);
    }
}
