using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class Edit
{
    [Inject]
    public UserService UserService { get; set; }
    public AddTargetAppFormViewModel Model { get; set; }

    [Parameter]
    public int TargetAppId { get; set; }

    protected override async void OnInitialized()
    {
        var targetApp = await UserService.GetTargetAppByIdAsync(TargetAppId);

        if (targetApp is null)
            NavigationManager.NavigateTo("/target-apps");

        Model = new()
        {
            Id = TargetAppId,
            MonitoringIntervalInSeconds = targetApp.MonitoingIntervalInSeconds,
            Name = targetApp.Name,
            UrlAddress = targetApp.UrlAddress,
        };
    }

    private async Task Submit() => await UserService.AddTargetAppAsync(Model);
}
