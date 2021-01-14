using AutoMapper;
using ZurumPark.Dtos;
using ZurumPark.Entities;

namespace ZurumPark.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
        }
    }
}