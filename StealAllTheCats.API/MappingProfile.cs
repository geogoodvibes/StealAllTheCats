using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Dto.Tags;

namespace StealAllTheCats.API;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        ////Cats
        //CreateMap<GetCatResponseDto, CatViewModel>()
        //    .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag.Name));

        //CreateMap<GetCatResponseDto, AddCatViewModel>();
        //CreateMap<GetCatResponseDto, AddCatRequestDto>()
        //    .ForMember(dest => dest.CvFilename, opt => opt.MapFrom(src => src.CV.FileName))
        //    .ForMember(dest => dest.CV, opt => opt.MapFrom(src => FileConverter.ConvertToBase64(src.CV)));

        ////Tags
        //CreateMap<GetTagResponseDto, TagViewModel>();
        //CreateMap<GetTagResponseDto, AddTagViewModel>();
        //CreateMap<AddTagViewModel, AddTagRequestDto>();
    }
}

