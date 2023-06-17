using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class Edit
{
    [Inject]
    public UserService UserService { get; set; }
    public TargetAppFormViewModel Model { get; set; }

    [Parameter]
    public int TargetAppId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = new();
        var targetApp = await UserService.GetTargetAppByIdAsync(TargetAppId);

        if (targetApp is null)
            NavigationManager.NavigateTo("/target-apps");

        Model.Id = TargetAppId;
        Model.MonitoringIntervalInSeconds = targetApp.MonitoringIntervalInSeconds;
        Model.Name = targetApp.Name;
        Model.UrlAddress = targetApp.UrlAddress;
    }

    private async Task Submit() => await UserService.UpdateTargetAppAsync(Model);
}
