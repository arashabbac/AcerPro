using AcerPro.Presentation.Client.Services;
using AcerPro.Presentation.Client.ViewModels;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AcerPro.Presentation.Client.Pages.TargetApps;

public partial class Index
{
    [Inject]
    public UserService UserService { get; set; }

    public IEnumerable<TargetAppViewModel> TargetApps { get; set; }
    private bool _loading = false;

    protected override async Task OnInitializedAsync()
    {
        await FetchTargetApps();
    }

    async Task FetchTargetApps()
    {
        _loading = true;
        TargetApps = await UserService.GetAllTargetAppsAsync();
        _loading = false;
    }

    async Task OnRowExpand(RowData<TargetAppViewModel> rowData)
    {
        if (rowData.Data.Notifiers is null)
        {
            return;
        }

        StateHasChanged();
    }

    async Task DeleteTargetAppAsync(int id) 
    {
        await UserService.DeleteTargetAppAsync(id);
        await FetchTargetApps();
    }
}
