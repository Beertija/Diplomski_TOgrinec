using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class StationService : IStationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Station> _repository;

    public StationService(IMapper mapper, IRepository<Station> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public List<StationDto> GetAll()
    {
        var stationList = _repository.GetAll();
        return _mapper.Map<List<StationDto>>(stationList);
    }
}