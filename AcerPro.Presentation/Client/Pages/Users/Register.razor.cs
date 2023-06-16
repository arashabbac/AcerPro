using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.Users;

public partial class Register
{
    [Inject]
    public UserService UserService { get; set; }

    public RegisterUserFormViewModel Model { get; set; }

    protected override void OnInitialized()
    {
        Model = new();
    }

    private async Task Submit() => await UserService.RegisterAsync(Model);
}
