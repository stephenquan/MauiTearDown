// CustomDiagnostics.cs

using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTearDown;

public partial class CustomDiagnostics : ObservableObject
{
    [ObservableProperty] public partial int CustomButtonConstructedCount { get; set; } = 0;
    [ObservableProperty] public partial int CustomButtonLoadedCount { get; set; } = 0;
    [ObservableProperty] public partial int CustomLabelConstructedCount { get; set; } = 0;
    [ObservableProperty] public partial int CustomLabelLoadedCount { get; set; } = 0;
}
