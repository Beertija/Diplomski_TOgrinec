using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface ITrainMaintenanceService
{
    Task<HttpResponseMessage> CreateOrUpdateTrainMaintenance(TrainMaintenanceDto trainMaintenance);

    Task<HttpResponseMessage> DeleteTrainMaintenance(Guid trainMaintenanceId);
}