using Film_API.Data.Entities;

namespace Film_API.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Franchise UpdateMovies(int[] movieIds, int franchiseId);
    }
}
