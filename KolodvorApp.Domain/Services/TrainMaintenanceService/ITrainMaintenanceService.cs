using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITrainMaintenanceService
{
    Task<TrainMaintenanceDto> CreateOrUpdateAsync(TrainMaintenanceDto trainMaintenaceDto);

    Task DeleteAsync(Guid id);
}