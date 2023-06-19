using AcerPro.Presentation.Client.ViewModels;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
        var response = await PutAsJsonAsync($"users", viewModel,true);

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

    public async Task AddTargetAppAsync(TargetAppFormViewModel viewModel)
    {
        var response = await PostAsJsonAsync($"users/target-app", viewModel,true);

        if (response.IsSuccess)
        {
            _navigationManager.NavigateTo("/target-apps");
        }
    }

    public async Task UpdateTargetAppAsync(TargetAppFormViewModel viewModel)
    {
        var response = await PutAsJsonAsync($"users/target-app/{viewModel.Id}", viewModel,true);

        if (response.IsSuccess)
        {
            _navigationManager.NavigateTo("/target-apps");
        }
    }

    public async Task DeleteTargetAppAsync(int id)
    {
        await DeleteAsync($"users/target-app/{id}", true);
    }

    public async Task<UserViewModel> GetCurrentUserAsync()
    {
        var response = await GetAsync<UserViewModel>($"users/get-current-user",true);

        return response.Data;
    }

    public async Task<TargetAppViewModel> GetTargetAppByIdAsync(int id)
    {
        var response = await GetAsync<TargetAppViewModel>($"users/target-app/{id}", true);

        return response.Data;
    }

    public async Task<List<TargetAppViewModel>> GetAllTargetAppsAsync()
    {
        var response = await GetAsync<List<TargetAppViewModel>>($"users/target-app", true);

        return response.Data;
    }

    public async Task AddNotifierAsync(NotifierFormViewModel viewModel)
    {
        var response = await PostAsJsonAsync($"users/target-app/notifier", viewModel,true);

        if (response.IsSuccess)
        {
            _navigationManager.NavigateTo("/target-apps");
        }
    }
}
