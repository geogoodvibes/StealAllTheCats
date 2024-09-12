

using StealAllTheCats.Data.Entities;
using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Dto.Tags;
using StealAllTheCats.Utilities;

namespace StealAllTheCats.API;
public class MappingProfile : AutoMapper.Profile
{
    /// <summary>
    /// Class MappingProfile.
    /// </summary>
    public MappingProfile()
    {

        //Cats
        CreateMap<GetCatApiResponseDto, AddCatRequestDto>()
            .ForMember(dest => dest.CatApiId, opt => opt.MapFrom(src => src.Id));

        CreateMap<AddCatRequestDto, Cat>();

        CreateMap<Cat, GetCatResponseDto>();

        CreateMap<GetCatResponseDto, Cat>();

        CreateMap<PaginatedResult<Cat>, PaginatedResult<GetCatResponseDto>>();


        //Breeds
        CreateMap<GetBreedResponseDto, AddBreedRequestDto>();


        //Tags
        CreateMap<GetTagResponseDto, Tag>();

        CreateMap<Tag, GetTagResponseDto>();
    }
}

