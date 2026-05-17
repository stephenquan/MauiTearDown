namespace MauiTearDown;

public partial class ThirdPage : ContentPage
{
    public ThirdPage(CustomButtonDiagnostics diagnostics)
    {
        BindingContext = diagnostics;
        InitializeComponent();
    }
}