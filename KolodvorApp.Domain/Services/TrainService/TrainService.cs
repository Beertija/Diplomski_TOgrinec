using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class TrainService : ITrainService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Train> _repository;

    public TrainService(IMapper mapper, IRepository<Train> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public List<TrainDto> GetAll()
    {
        var trainList = _repository.GetAll();
        return _mapper.Map<List<TrainDto>>(trainList);
    }

    public async Task<TrainDto> GetAsync(Guid id)
    {
        var train = await _repository.GetAsync(id);
        return _mapper.Map<TrainDto>(train);
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