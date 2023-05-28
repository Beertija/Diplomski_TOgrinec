using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KolodvorApp.Client.Shared;

public abstract class PageBase : ComponentBase
{
    [Inject]
    public ISnackbar _snackbar { get; set; }

    [Inject]
    public NavigationManager _navigation { get; set; }
}