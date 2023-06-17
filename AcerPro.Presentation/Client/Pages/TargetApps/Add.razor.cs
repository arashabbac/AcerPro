using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class Add
{
    [Inject]
    public UserService UserService { get; set; }
    public AddTargetAppFormViewModel Model { get; set; }

    protected override void OnInitialized()
    {
        Model = new();
    }

    private async Task Submit() => await UserService.AddTargetAppAsync(Model);
}
