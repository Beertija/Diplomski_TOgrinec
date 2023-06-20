using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface ITrainService
{
    Task<List<TrainDto>> GetAll();
}