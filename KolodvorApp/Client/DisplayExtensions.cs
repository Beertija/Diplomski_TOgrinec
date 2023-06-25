namespace KolodvorApp.Client;

public static class DisplayExtensions
{
    public static string DisplayDecimal(this decimal value) => value.ToString("F2");
}