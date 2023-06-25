using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface ITrainService
{
    Task<List<TrainDto>> GetAll();

    Task<List<TrainCategoryDto>> GetAllCategories();

    Task<TrainDto> GetTrainByIdWithMaintenances(Guid trainId);

    Task<HttpResponseMessage> CreateOrUpdateTrain(TrainDto train);

    Task<HttpResponseMessage> DeleteTrain(Guid trainId);
}