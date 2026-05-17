// CustomBalanceLabel.cs

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

namespace MauiTearDown;

public partial class CustomBalanceLabel : Label
{
    [BindableProperty] public partial decimal Balance { get; set; } = 0.0m;

    CustomDiagnostics diagnostics;
    static int nextId = 0;
    int instanceId = Interlocked.Increment(ref nextId);
    int loadedCount = 0;
    int unloadedCount = 0;

    public CustomBalanceLabel()
    {
        Trace.WriteLine($"Instance:{instanceId} Constructor");
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        diagnostics = IPlatformApplication.Current?.Services.GetRequiredService<CustomDiagnostics>() ?? throw new InvalidOperationException("CustomButtonDiagnostics service not found.");
        diagnostics.CustomLabelConstructedCount++;

        // Use bindings wherever possible (minimize the use of event handlers):
        // 1. follows established MVVM patterns.
        // 2. safe UI updates via binding (framework marshals to UI thread).
        // 3. fewer memory lifecycle issues.
        // 4. automatic UI synchronization.
        // 5. easier to test.
        this.SetBinding(TextProperty,
            static (CustomBalanceLabel view) => view.Balance,
            BindingMode.OneWay,
            stringFormat: "Balance: {0:C}",
            source: this);
        this.SetBinding(TextColorProperty,
            static (CustomBalanceLabel view) => view.Balance,
            BindingMode.OneWay,
            converter: new FuncConverter<decimal, Color>(balance => balance >= 0 ? Colors.Green : Colors.Red),
            source: this);
    }

    ~CustomBalanceLabel()
    {
        Trace.WriteLine($"Instance:{instanceId} Destructor");
        diagnostics.CustomLabelConstructedCount--;
    }

    void OnLoaded(object? sender, EventArgs e)
    {
        Trace.WriteLine($"Instance:{instanceId} Loaded (Loaded:{++loadedCount} Unloaded:{unloadedCount} BindingContext:{BindingContext.GetHashCode()})");
        diagnostics.CustomLabelLoadedCount++;

        // Defend against duplicate subscriptions (Hot Reload, reattach).
        PropertyChanged -= SafePropertyChanged;
        PropertyChanged += SafePropertyChanged;
    }

    void OnUnloaded(object? sender, EventArgs e)
    {
        Trace.WriteLine($"Instance:{instanceId} Unloaded (Loaded:{loadedCount} Unloaded:{++unloadedCount})");
        diagnostics.CustomLabelLoadedCount--;

        // Unsubscribe from events to prevent memory leaks and unintended behavior.
        PropertyChanged -= SafePropertyChanged;
    }

    void SafePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Marshall to the UI thread if necessary, as PropertyChanged may be raised on a background thread.
        if (Dispatcher.IsDispatchRequired)
        {
            Dispatcher.Dispatch(() =>
            {
                PropertyChangedCore(sender, e);
            });
            return;
        }

        // Direct UI access is safe on the UI thread.
        PropertyChangedCore(sender, e);
    }

    void PropertyChangedCore(object? sender, PropertyChangedEventArgs e)
    {
        this.FontAttributes = Balance >= 0 ? FontAttributes.None : FontAttributes.Italic;
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Alternate to Bindings and EventHandlers, consider using override OnPropertyChanged to
        // void worrying about event subscriptions and memory leaks.
    }
}
