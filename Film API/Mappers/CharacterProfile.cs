using AutoMapper;
using Film_API.Data.DTOs.Character;
using Film_API.Data.DTOs.Movies;
using Film_API.Data.Entities;

namespace Film_API.Mappers
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDetailDTO>()
                .ForMember(c => c.Movies,
                           options => options.MapFrom(c => c.Movies.Select(m => m.Id).ToArray()));

            CreateMap<Character, CharacterBriefDTO>();

            CreateMap<CharacterPutDTO, Character>();

            CreateMap<CharacterPostDTO, Character>().ReverseMap();
        }
    }
}
