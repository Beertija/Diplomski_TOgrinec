using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IStationService
{
    List<StationDto> GetAll();
}