using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITrainMaintenanceService
{
    Task<List<TrainMaintenanceDto>> GetAllByTrainIdAsync(Guid trainId);

    Task<TrainMaintenanceDto> CreateOrUpdateAsync(TrainMaintenanceDto trainMaintenaceDto);

    Task DeleteAsync(Guid id);
}