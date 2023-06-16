using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.Users;

public partial class Profile
{
    [Inject]
    public TokenService TokenService { get; set; }

    public UserViewModel User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await FetchCustomersAsync();
    }

    private async Task FetchCustomersAsync()
    {
        User = await TokenService.GetUserAsync();
    }
}
