using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Client.Pages.Users;

public partial class Edit
{
    [Inject]
    public UserService UserService { get; set; }

    public RegisterUserFormViewModel Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = new();

        var data =
            await UserService.GetCurrentUserAsync();

        Model =
            new()
            {
                Id = data.Id,
                Email = data.Email,
                Lastname = data.Lastname,
                Firstname = data.Firstname,
            };
    }

    private async Task Submit() => await UserService.UpdateAsync(Model);
}