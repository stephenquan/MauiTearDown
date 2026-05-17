// ThirdPage.xaml.cs

namespace MauiTearDown;

public partial class ThirdPage : ContentPage
{
    public ThirdPage(CustomDiagnostics diagnostics)
    {
        BindingContext = diagnostics;
        InitializeComponent();
    }
}