using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KolodvorApp.Client.Shared;

public static class RenderTrainCategoryIcons
{
    private static Dictionary<string, object> categoryIconMapping = new()
    {
        { "Vagoni drugog razreda", Icons.Material.Filled.LooksTwo },
        { "Vagoni prvog razreda", Icons.Material.Filled.LooksOne },
        { "Vagoni za prijevoz bicikla", Icons.Material.Filled.PedalBike },
        { "Putnički vlak", Icons.Material.Filled.Train },
        { "Brzi vlak", Icons.Material.Filled.DirectionsRailway },
        { "Vagon s mjestima za osobe s invaliditetom", Icons.Material.Filled.Accessible }
    };

    public static RenderFragment IconDisplay(List<ContainsDto> categories) => builder =>
    {
        int seq = 0;
        foreach (var category in categories)
        {
            builder.OpenComponent<MudTooltip>(seq++);
            builder.AddAttribute(seq++, "Text", category.TrainCategory!.Name);
            builder.AddAttribute(seq++, "ChildContent", new RenderFragment(b =>
            {
                int nestedSeq = 0;
                if (categoryIconMapping.TryGetValue(category.TrainCategory.Name, out var icon))
                {
                    b.OpenComponent<MudIcon>(nestedSeq++);
                    b.AddAttribute(nestedSeq++, "Style", "fill: var(--secondary-color); margin-right: 1.5em;");
                    b.AddAttribute(nestedSeq++, "Icon", icon);
                    b.CloseComponent();
                }
            }));
            builder.CloseComponent();
        }
    };
}