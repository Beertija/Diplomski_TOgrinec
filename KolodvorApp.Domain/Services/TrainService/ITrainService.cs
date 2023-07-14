using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITrainService
{
    List<TrainDto> GetAll();

    List<TrainSelectorDto> GetAllTrainsForSelect();

    Task<TrainDto> GetAsync(Guid id, bool includeMaintaining);

    Task<Train> GetSpecificAsync(Guid id);

    Task<TrainDto> CreateOrUpdateAsync(TrainDto trainDto);

    Task DeleteAsync(Guid id);
}