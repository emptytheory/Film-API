using Film_API.Data;
using Film_API.Data.Entities;

namespace Film_API.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly FilmDbContext _context;

        public CharacterService( FilmDbContext context ) 
        {
            _context = context;
        }

        public Task<Character> AddAsync(Character entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Character>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Character> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Character> UpdateAsync(Character entity)
        {
            throw new NotImplementedException();
        }
    }
}
