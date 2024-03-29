﻿using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface IUserService
{
    Task<List<UserDto>> GetAllEmployees();

    Task<List<UserDto>> GetAllRegularUsers();

    Task<HttpResponseMessage> PromoteUser(Guid userId);

    Task<HttpResponseMessage> DemoteUser(Guid userId);

    Task<HttpResponseMessage> Register(RegisterUserDto user);

    Task<HttpResponseMessage> Login(LoginUserDto user);
}