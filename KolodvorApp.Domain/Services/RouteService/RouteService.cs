using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class RouteService : IRouteService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Route> _repository;
    private readonly IRepository<RouteStation> _routeStationrepository;

    public RouteService(IMapper mapper, IRepository<Route> repository, IRepository<RouteStation> routeStationrepository)
    {
        _mapper = mapper;
        _repository = repository;
        _routeStationrepository = routeStationrepository;
    }

    public List<RouteDto> GetAll()
    {
        var routeList = _repository.GetAll();
        return _mapper.Map<List<RouteDto>>(routeList);
    }

    public List<RouteStationDto> GetAllRouteStations()
    {
        var routeList = _routeStationrepository.GetAll();
        return _mapper.Map<List<RouteStationDto>>(routeList);
    }
}