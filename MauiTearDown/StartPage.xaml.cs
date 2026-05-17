// StartPage.xaml.cs

namespace MauiTearDown;

public partial class StartPage : ContentPage
{
    public StartPage(CustomButtonDiagnostics diagnostics)
    {
        BindingContext = diagnostics;
        InitializeComponent();
    }

    async void OnStart(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}
