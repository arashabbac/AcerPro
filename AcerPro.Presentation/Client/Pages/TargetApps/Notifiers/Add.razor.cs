using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.TargetApps.Notifiers;

public partial class Add
{
    [Parameter]
    public int TargetAppId { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public NotifierFormViewModel Model { get; set; }

    protected override void OnInitialized()
    {
        Model = new()
        {
            TargetAppId = TargetAppId
        };
    }

    private async Task Submit()
    {
        await UserService.AddNotifierAsync(Model);
    }
}
