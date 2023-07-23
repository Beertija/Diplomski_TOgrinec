using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared;
using KolodvorApp.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

    public LoggedUserDto Login(LoginUserDto userDto)
    {
        try
        {
            var user = _repository.GetAll().Single(x => (x.Name.Equals(userDto.EmailOrUsername) || x.Email.Equals(userDto.EmailOrUsername)) && x.Password.Equals(userDto.Password));
            var model = _mapper.Map<LoggedUserDto>(user);
            model.JwtToken = CreateToken(model);
            return model;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Wrong credentials.");
        }
    }

    private static string CreateToken(LoggedUserDto user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1));

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}