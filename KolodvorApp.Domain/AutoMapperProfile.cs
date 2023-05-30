using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Train, TrainDto>()
            .ForMember(dest => dest.Maintenances,
            opt => opt.MapFrom(src => src.Maintenances.Select(model => new TrainMaintenanceDto
            {
                Id = model.Id,
                Cost = model.Cost,
                Maintenance = model.Maintenance
            })))
            .ForMember(dest => dest.Categories,
            opt => opt.MapFrom(src => src.Categories.Select(model => new ContainsDto
            {
                TrainId = model.TrainId,
                TrainCategoryId = model.TrainCategoryId,
            })))
            .ReverseMap();

        CreateMap<TrainMaintenance, TrainMaintenanceDto>().ReverseMap();
        CreateMap<TrainCategory, TrainCategoryDto>().ReverseMap();
        CreateMap<Station, StationDto>().ReverseMap();
    }
}