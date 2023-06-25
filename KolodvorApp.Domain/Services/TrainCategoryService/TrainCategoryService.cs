using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class TrainCategoryService : ITrainCategoryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<TrainCategory> _repository;

    public TrainCategoryService(IMapper mapper, IRepository<TrainCategory> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public List<TrainCategoryDto> GetAll()
    {
        var trainCategoriesList = _repository.GetAll();
        return _mapper.Map<List<TrainCategoryDto>>(trainCategoriesList);
    }

    public async Task<TrainCategoryDto> GetAsync(Guid id)
    {
        var trainCategory = await _repository.GetAsync(id);
        return _mapper.Map<TrainCategoryDto>(trainCategory);
    }
}