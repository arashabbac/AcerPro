﻿using AcerPro.Presentation.Client.ViewModels;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Services;

public class UserService : BaseHttpClientService
{
    private readonly NavigationManager _navigationManager;
    private readonly TokenService _tokenService;

    public UserService(HttpClient httpClient,
        NotificationService notificationService,
        TokenService tokenService,
        NavigationManager navigationManager) : base(httpClient,tokenService,notificationService)
    {
        _navigationManager = navigationManager;
        _tokenService = tokenService;
    }


    public bool Loading { get; set; }

    public async Task RegisterAsync(RegisterUserFormViewModel viewModel)
    {
        var response = await PostAsJsonAsync("users/register", value: viewModel);

        if(response.IsSuccess)
        {
            _navigationManager.NavigateTo("/login");
        }
    }

    public async Task UpdateAsync(RegisterUserFormViewModel viewModel)
    {
        var response = await PutAsJsonAsync($"users/update", viewModel,true);

        if (response.IsSuccess)
            _navigationManager.NavigateTo("/");
    }

    public async Task LoginAsync(LoginViewModel viewModel)
    {
        var response = await PostAsJsonAsync<LoginViewModel,UserViewModel>($"users/Login", viewModel);

        if (response.IsSuccess)
        {
            await _tokenService.SetAsync(response.Data);
            _navigationManager.NavigateTo("/");
        }
    }

    public async Task<UserViewModel> GetCurrentUserAsync()
    {
        var response = await GetAsync<UserViewModel>($"users/get-current-user",true);

        return response.Data;
    }
}