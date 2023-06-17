using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class Index
{
    [Inject]
    public UserService UserService { get; set; }

    public IReadOnlyList<TargetAppViewModel> TargetApps { get; set; }
}
