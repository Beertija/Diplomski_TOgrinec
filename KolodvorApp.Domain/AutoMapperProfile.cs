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
            .ReverseMap()
            .ForMember(dest => dest.Categories,
            opt => opt.MapFrom(src => src.Categories.Select(model => new Contains
            {
                TrainCategoryId = model.TrainCategoryId,
            })));
        CreateMap<Train, Train>();
        CreateMap<Train, TrainSelectorDto>();

        CreateMap<TrainMaintenance, TrainMaintenanceDto>().ReverseMap();
        CreateMap<TrainCategory, TrainCategoryDto>().ReverseMap();
        CreateMap<Station, StationDto>().ReverseMap();
        CreateMap<RouteStation, RouteStationDto>().ReverseMap();

        CreateMap<Route, RouteDto>()
            .ForMember(dest => dest.RouteStations,
            opt => opt.MapFrom(src => src.RouteStations.Select(model => new RouteStationDto
            {
                Id = model.Id,
                Cost = model.Cost,
                ArrivalTime = model.ArrivalTime,
                DepartureTime = model.DepartureTime,
                StartStation = new StationDto
                {
                    Id = model.StartStation.Id,
                    Name = model.StartStation.Name
                },
                StartStationId = model.StartStationId,
                EndStation = new StationDto
                {
                    Id = model.EndStation.Id,
                    Name = model.EndStation.Name
                },
                EndStationId = model.EndStationId,
                Order = model.Order
            })))
            .ForMember(dest => dest.TrainTag, opt => opt.MapFrom(src => src.Train.Tag))
            .ForMember(dest => dest.StartStation, opt => opt.MapFrom(src => src.RouteStations.OrderBy(x => x.Order).First().StartStation.Name))
            .ForMember(dest => dest.EndStation, opt => opt.MapFrom(src => src.RouteStations.OrderBy(x => x.Order).Last().EndStation.Name))
            .ReverseMap()
            .ForMember(dest => dest.Train, opt => opt.Ignore())
            .ForMember(dest => dest.RouteStations,
            opt => opt.MapFrom(src => src.RouteStations.Select(model => new RouteStation
            {
                Id = model.Id,
                Cost = model.Cost,
                ArrivalTime = model.ArrivalTime,
                DepartureTime = model.DepartureTime,
                StartStationId = model.StartStationId,
                EndStationId = model.EndStationId,
                Order = model.Order
            })));
        CreateMap<Route, Route>();

        CreateMap<User, UserDto>();
    }
}