using AutoMapper;
using ProjectAPI.Models.Domain;
using ProjectAPI.Models.DTOs;

namespace ProjectAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap(); 
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();  
            CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        
        }
    }
}
