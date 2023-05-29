using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class TrainMaintenanceService : ITrainMaintenanceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<TrainMaintenance> _repository;
    private readonly ITrainService _trainService;

    public TrainMaintenanceService(IMapper mapper, IRepository<TrainMaintenance> repository, ITrainService trainService)
    {
        _mapper = mapper;
        _repository = repository;
        _trainService = trainService;
    }

    public async Task<List<TrainMaintenanceDto>> GetAllByTrainIdAsync(Guid trainId)
    {
        await CheckIfExistsAsync(trainId);
        var trainMaintenanceList = _repository.GetAll().Where(x => x.TrainId == trainId);
        return _mapper.Map<List<TrainMaintenanceDto>>(trainMaintenanceList);
    }

    public async Task<TrainMaintenanceDto> CreateOrUpdateAsync(TrainMaintenanceDto trainMaintenaceDto)
    {
        var trainMaintenace = _mapper.Map<TrainMaintenance>(trainMaintenaceDto);
        await CheckIfExistsAsync(trainMaintenace.TrainId);

        if (trainMaintenaceDto.Id is null)
        {
            trainMaintenace = await _repository.InsertAsync(trainMaintenace);
        }
        else
        {
            try
            {
                await _repository.GetAsync(trainMaintenace.Id);
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException("Tried to update an non-existing entity.");
            }
            trainMaintenace = await _repository.UpdateAsync(trainMaintenace);
        }

        return _mapper.Map<TrainMaintenanceDto>(trainMaintenace);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    private async Task CheckIfExistsAsync(Guid trainId)
    {
        try
        {
            await _trainService.GetAsync(trainId, false);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException($"Train {trainId} does not exist.");
        }
    }
}