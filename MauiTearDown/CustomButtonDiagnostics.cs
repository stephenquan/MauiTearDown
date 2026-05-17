using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTearDown;

public partial class CustomButtonDiagnostics : ObservableObject
{
    [ObservableProperty]
    public partial int CustomButtonConstructedCount { get; set; } = 0;

    [ObservableProperty]
    public partial int CustomButtonLoadedCount { get; set; } = 0;
}
