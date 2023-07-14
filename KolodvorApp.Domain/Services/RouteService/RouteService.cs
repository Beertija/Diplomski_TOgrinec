using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class RouteService : IRouteService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Route> _repository;
    private readonly ITrainService _trainService;

    public RouteService(IMapper mapper, IRepository<Route> repository, ITrainService trainService)
    {
        _mapper = mapper;
        _repository = repository;
        _trainService = trainService;
    }

    public List<RouteDto> GetAll()
    {
        var routeList = _repository.GetAll();
        return _mapper.Map<List<RouteDto>>(routeList);
    }

    public async Task<RouteDto> CreateOrUpdateAsync(RouteDto routeDto)
    {
        var route = _mapper.Map<Route>(routeDto);

        if (routeDto.Id is null)
        {
            route = await _repository.InsertAsync(route);
        }
        else
        {
            try
            {
                var entity = await _repository.GetAsync(route.Id);
                _mapper.Map(route, entity);
                entity.Train = await _trainService.GetSpecificAsync(route.TrainId);
                route = await _repository.UpdateAsync(entity);
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException("Tried to update an non-existing entity.");
            }
        }
        route = await _repository.GetAsync(route.Id);
        return _mapper.Map<RouteDto>(route);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}