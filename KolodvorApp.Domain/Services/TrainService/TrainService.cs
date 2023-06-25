using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class TrainService : ITrainService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Train> _repository;
    private readonly ITrainCategoryService _categoryService;

    public TrainService(IMapper mapper, IRepository<Train> repository, ITrainCategoryService categoryService)
    {
        _mapper = mapper;
        _repository = repository;
        _categoryService = categoryService;
    }

    public List<TrainDto> GetAll()
    {
        var trainList = _repository.GetAll();
        var modelList = _mapper.Map<List<TrainDto>>(trainList);

        var trainCategories = _categoryService.GetAll();

        foreach (var train in modelList)
        {
            foreach (var category in train.Categories)
            {
                category.TrainCategory = trainCategories.Single(x => x.Id == category.TrainCategoryId);
            }
        }

        return modelList;
    }

    public async Task<TrainDto> GetAsync(Guid id, bool includeMaintaining)
    {
        var train = (includeMaintaining
            ? _repository.WithDetails(x => x.Maintenances).Where(x => x.Id == id).SingleOrDefault()
            : await _repository.GetAsync(id))
            ?? throw new KeyNotFoundException();

        var model = _mapper.Map<TrainDto>(train);
        foreach (var category in model.Categories)
        {
            category.TrainCategory = await _categoryService.GetAsync(category.TrainCategoryId);
        }

        return model;
    }

    public async Task<TrainDto> CreateOrUpdateAsync(TrainDto trainDto)
    {
        var train = _mapper.Map<Train>(trainDto);

        if (trainDto.Id is null)
        {
            train = await _repository.InsertAsync(train);
        }
        else
        {
            try
            {
                await _repository.GetAsync(train.Id);
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException("Tried to update an non-existing entity.");
            }
            train = await _repository.UpdateAsync(train);
        }

        return _mapper.Map<TrainDto>(train);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}