using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;

    public UserService(IMapper mapper, IRepository<User> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public List<UserDto> GetAllEmployees()
    {
        var usersList = _repository.GetAll().Where(x => x.Role == Role.Worker);
        return _mapper.Map<List<UserDto>>(usersList);
    }

    public List<UserDto> GetAllRegularUsers()
    {
        var usersList = _repository.GetAll().Where(x => x.Role == Role.User);
        return _mapper.Map<List<UserDto>>(usersList);
    }
}