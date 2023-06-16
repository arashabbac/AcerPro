using Microsoft.JSInterop;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using AcerPro.Common;
using System.Net.Http.Json;
using AntDesign;

namespace AcerPro.Presentation.Client.Services;

public class BaseHttpClientService
{
    private readonly TokenService _tokenService;
    private readonly NotificationService _notificationService;

    public BaseHttpClientService
        (HttpClient client,
        TokenService tokenService,
        NotificationService notificationService)
    {
        Client = client;
        _tokenService = tokenService;
        _notificationService = notificationService; 
    }

    protected HttpClient Client { get; }

    public async Task<APIResponseModel>
        PostAsJsonAsync<TValue>(string requestUri, TValue value, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PostAsJsonAsync(requestUri, value);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel>();

            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }

    }

    public async Task<APIResponseModel> PostAsync(string requestUri, HttpContent httpContent = null, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PostAsync(requestUri, httpContent);
            
            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel>();

            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    public async Task<APIResponseModel<TResponse>>
        PostAsync<TResponse>(string requestUri, HttpContent httpContent = null, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PostAsync(requestUri, httpContent);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel<TResponse>>();

            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }

    }

    public async Task<APIResponseModel<TResponse>>
        PostAsJsonAsync<TValue, TResponse>(string requestUri, TValue value, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PostAsJsonAsync(requestUri, value);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel<TResponse>>();

            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    public async Task<APIResponseModel>
        PutAsJsonAsync<TValue>(string requestUri, TValue value, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PutAsJsonAsync(requestUri, value);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel>();

            return result!;
        }
        catch(Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    public async Task<APIResponseModel<TResponse>>
        PutAsJsonAsync<TValue, TResponse>
        (string requestUri, TValue value, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.PutAsJsonAsync(requestUri, value);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel<TResponse>>();

            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    public async Task DeleteAsync
        (string requestUri, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.DeleteAsync(requestUri);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
            }

            await ShowSuccessMessage(response);

        }
        catch (Exception ex)
        {
            await ShowErrorMessage(ex.Message);
        }
    }

    public async Task
        <APIResponseModel<TResponse>> GetAsync<TResponse>
        (string requestUri, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel<TResponse>>();

            return result!;

        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    public async Task<APIResponseModel> GetAsync(string requestUri, bool hasAuthorize = false)
    {
        try
        {
            if (hasAuthorize)
            {
                await SetAuthenticationHeaderAsync();
            }

            var response = await Client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode == false)
            {
                await ShowErrorMessage(response);
                return null;
            }

            var result =
                await response.Content.ReadFromJsonAsync<APIResponseModel>();


            return result!;
        }
        catch (Exception e)
        {
            await ShowErrorMessage(e.Message);
            return null;
        }
    }

    protected async Task SetAuthenticationHeaderAsync()
    {
        var user = await _tokenService.GetUserAsync();

        if (user != null)
        {
            Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue
                    ("Bearer", user.Token);
        }
    }

    private async Task ShowErrorMessage(HttpResponseMessage response)
    {
        foreach (var message in (await response.Content.ReadFromJsonAsync<APIResponseModel>()).Messages)
        {
            if (string.IsNullOrWhiteSpace(message) == false)
                await _notificationService.Error(new NotificationConfig
                {
                    Message = message,
                    NotificationType = NotificationType.Error,
                    Placement = NotificationPlacement.TopRight,
                    Duration = 3
                });
        }
    }

    private async Task ShowSuccessMessage(HttpResponseMessage response)
    {
        foreach (var message in (await response.Content.ReadFromJsonAsync<APIResponseModel>()).Messages)
        {
            if (string.IsNullOrWhiteSpace(message) == false)
                await _notificationService.Success(new NotificationConfig
                {
                    Message = message,
                    NotificationType = NotificationType.Success,
                    Placement = NotificationPlacement.TopRight,
                    Duration = 2.5
                });
        }
    }

    private async Task ShowErrorMessage(string message)
    {
        await _notificationService.Error(new NotificationConfig
        {
            Message = message,
            NotificationType = NotificationType.Error,
            Placement = NotificationPlacement.TopRight,
            Duration = 3
        });
    }

    private async Task ShowSuccessMessage(string message)
    {
        await _notificationService.Success(new NotificationConfig
        {
            Message = message,
            NotificationType = NotificationType.Success,
            Placement = NotificationPlacement.TopRight,
            Duration = 2.5
        });
    }
}
