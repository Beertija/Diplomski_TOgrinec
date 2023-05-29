using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITrainCategoryService
{
    List<TrainCategoryDto> GetAll();

    Task<TrainCategoryDto> GetAsync(Guid id);
}