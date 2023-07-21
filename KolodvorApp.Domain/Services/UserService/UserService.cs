using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared;
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
        var usersList = _repository.GetAll().Where(x => x.Role == RoleEnum.Worker);
        return _mapper.Map<List<UserDto>>(usersList);
    }

    public List<UserDto> GetAllRegularUsers()
    {
        var usersList = _repository.GetAll().Where(x => x.Role == RoleEnum.User);
        return _mapper.Map<List<UserDto>>(usersList);
    }

    public async Task PromoteUser(Guid userId)
    {
        try
        {
            var user = await _repository.GetAsync(userId);
            user.Role = RoleEnum.Worker;
            await _repository.UpdateAsync(user);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("Tried to update an non-existing entity.");
        }
    }

    public async Task DemoteUser(Guid userId)
    {
        try
        {
            var user = await _repository.GetAsync(userId);
            user.Role = RoleEnum.User;
            await _repository.UpdateAsync(user);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("Tried to update an non-existing entity.");
        }
    }

    public async Task Register(RegisterUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _repository.InsertAsync(user);
    }

    public UserDto Login(LoginUserDto userDto)
    {
        try
        {
            var user = _repository.GetAll().Single(x => (x.Name.Equals(userDto.EmailOrUsername) || x.Email.Equals(userDto.EmailOrUsername)) && x.Password.Equals(userDto.Password));
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Wrong credentials.");
        }
    }
}