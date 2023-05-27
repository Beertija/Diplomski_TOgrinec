using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITrainService
{
    List<TrainDto> GetAll();

    Task<TrainDto> GetAsync(Guid id);

    Task<TrainDto> CreateOrUpdateAsync(TrainDto trainDto);

    Task DeleteAsync(Guid id);
}