using AutoMapper;
using Film_API.Data.DTOs.Movie;
using Film_API.Data.Entities;

namespace Film_API.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDetailDTO>()
                .ForMember(m => m.Characters,
                           options => options.MapFrom(m => m.Characters.Select(c => c.Id).ToArray()));

            CreateMap<Movie, MovieBriefDTO>();

            CreateMap<MoviePutDTO, Movie>();

            CreateMap<MoviePostDTO, Movie>().ReverseMap();

        }
    }
}
