using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class FormEditor
{
    [Parameter]
    public EventCallback OnValidSubmit { get; set; }

    [Parameter]
    public AddTargetAppFormViewModel Model { get; set; }

    private void HandleValidSubmit()
    {
        OnValidSubmit.InvokeAsync(null);
    }
}
