// MauiProgram.cs

using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace MauiTearDown;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ThirdPage), typeof(ThirdPage));

        builder.Services.AddTransient<StartPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ThirdPage>();
        builder.Services.AddSingleton<CustomDiagnostics>();

        return builder.Build();
    }
}
