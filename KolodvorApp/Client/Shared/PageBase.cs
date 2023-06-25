using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KolodvorApp.Client.Shared;

public abstract class PageBase : ComponentBase
{
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;
}