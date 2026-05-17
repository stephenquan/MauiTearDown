// CityInfo.cs

using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTearDown;

public partial class CityInfo : ObservableObject
{
    [ObservableProperty] public partial int Id { get; set; } = 0;
    [ObservableProperty] public partial string Name { get; set; } = string.Empty;
}
