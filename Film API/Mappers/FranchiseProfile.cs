using AutoMapper;
using Film_API.Data.DTOs.Franchises;
using Film_API.Data.Entities;

namespace Film_API.Mappers
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDetailDTO>()
                .ForMember(f => f.Movies,
                           options => options.MapFrom(f => f.Movies.Select(m => m.Id).ToArray()));

            CreateMap<Franchise, FranchiseBriefDTO>();

            CreateMap<FranchisePutDTO, Franchise>();

            CreateMap<FranchisePostDTO, Franchise>().ReverseMap();

        }
    }
}
