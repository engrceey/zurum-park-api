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
        }
    }
}