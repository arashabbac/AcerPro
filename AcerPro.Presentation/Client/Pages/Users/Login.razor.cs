using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.Users;

public partial class Login
{
    [Inject]
    public UserService UserService { get; set; }
    public LoginViewModel Model { get; set; }
    private bool _loading;

    protected override void OnInitialized()
    {
        Model = new();
    }

    private async Task LoginSubmit()
    {
        _loading = true;

        await UserService.LoginAsync(Model);

        _loading = false;
    }
}
