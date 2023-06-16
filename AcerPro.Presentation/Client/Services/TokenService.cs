using AcerPro.Presentation.Client.ViewModels;
using Microsoft.JSInterop;
using System.Text.Json;

namespace AcerPro.Presentation.Client.Services;

public class TokenService
{
    private readonly IJSRuntime _jSRuntime;
    private readonly Microsoft.AspNetCore.Components.NavigationManager _navigationManager;

    public TokenService
        (IJSRuntime jSRuntime,
        Microsoft.AspNetCore.Components.NavigationManager navigationManager)
    {
        _jSRuntime = jSRuntime;
        _navigationManager = navigationManager;
    }

    public UserViewModel User { get; set; } = new();

    public async System.Threading.Tasks.Task
        SetAsync(UserViewModel login)
    {
        var json = JsonSerializer.Serialize(login);

        await _jSRuntime.InvokeVoidAsync
            ("localStorage.setItem", "user", json)
            .ConfigureAwait(false);

        _navigationManager.NavigateTo("/");
    }

    public async System.Threading.Tasks.Task<UserViewModel>
        GetUserAsync()
    {
        var json =
            await _jSRuntime.InvokeAsync<string>
            ("localStorage.getItem", "user")
            .ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(json))
        {
            _navigationManager.NavigateTo(uri: "/login", forceLoad: false);
            return null;
        }

        var response = JsonSerializer.Deserialize<UserViewModel>(json);

        if (response.ExpireIn < System.DateTime.UtcNow)
        {
            await RemoveAsync();
            return null;
        }

        User = response;
        return response;
    }


    public async System.Threading.Tasks.Task RemoveAsync()
    {
        await _jSRuntime
            .InvokeVoidAsync("localStorage.removeItem", "user")
            .ConfigureAwait(false);

        _navigationManager.NavigateTo("/login", forceLoad: true);
    }

}

