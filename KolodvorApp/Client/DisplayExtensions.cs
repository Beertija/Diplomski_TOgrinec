namespace KolodvorApp.Client;

public static class DisplayExtensions
{
    public static string DisplayDecimal(this decimal value) => value.ToString("F2");

    public static string DisplayDuration(this TimeSpan value) => value.ToString("hh\\:mm");

    public static string DisplayTime(this TimeOnly value) => value.ToString("HH:mm");
}