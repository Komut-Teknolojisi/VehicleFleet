using AutoMapper;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName))
                ;

            CreateMap<Brand, BrandDto>();


            CreateMap<Model, ModelDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                ;
        }
    }
}
