using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface IStationService
{
    Task<List<StationDto>> GetAll();
}