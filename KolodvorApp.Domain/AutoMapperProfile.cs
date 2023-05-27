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
            .ReverseMap();
        CreateMap<TrainMaintenance, TrainMaintenanceDto>().ReverseMap();
    }
}