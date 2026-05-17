// MainPage.xaml.cs

using System.Collections.ObjectModel;
using System.Text;

namespace MauiTearDown;

public partial class MainPage : ContentPage
{
    public CustomDiagnostics Diagnostics { get; }
    public ObservableCollection<CityInfo> Cities { get; } = new();

    public MainPage(CustomDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
        BindingContext = this;
        InitializeComponent();
    }

    static string[] cityPrefixes = { "New", "Old", "North", "South", "East", "West" };
    static string[] cityNames = { "Spring", "Lake", "Port", "Green", "Hill", "River" };
    static string[] citySuffixes = { "ton", "burg", "ford", "field", "wood", "dale" };

    void OnAdd(object? sender, EventArgs e)
    {
        int id = Cities.Count + 1;
        string cityName =
            new StringBuilder()
            .Append(cityPrefixes[Random.Shared.Next(cityPrefixes.Length)])
            .Append(' ')
            .Append(cityNames[Random.Shared.Next(cityNames.Length)])
            .Append(citySuffixes[Random.Shared.Next(citySuffixes.Length)])
            .ToString();
        decimal tradeBalance = (decimal)(Random.Shared.NextDouble() * 1000000 - 500000);
        Cities.Add(new CityInfo { Id = id, Name = cityName, TradeBalance = tradeBalance });
    }

    void OnRemove(object? sender, EventArgs e)
    {
        if (Cities.Count > 0)
        {
            Cities.RemoveAt(Cities.Count - 1);
        }
    }

    void OnNext(object? sender, EventArgs e)
    {
        _ = Shell.Current.GoToAsync(nameof(ThirdPage));
    }

    async void OnGC(object sender, EventArgs e)
    {
        GC.Collect();
    }
}
