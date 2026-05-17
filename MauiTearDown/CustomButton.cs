// CustomButton.cs

using System.Diagnostics;

namespace MauiTearDown;

public partial class CustomButton : Button
{
    CustomButtonDiagnostics diagnostics;
    static int nextId = 0;
    int instanceId = Interlocked.Increment(ref nextId);
    int loadedCount = 0;
    int unloadedCount = 0;
    public CustomButton()
    {
        Trace.WriteLine($"Instance:{instanceId} Constructor");
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        diagnostics = IPlatformApplication.Current?.Services.GetRequiredService<CustomButtonDiagnostics>() ?? throw new InvalidOperationException("CustomButtonDiagnostics service not found.");
        diagnostics.CustomButtonConstructedCount++;

        // Use bindings wherever possible (minimize the use of event handlers):
        // 1. follows established MVVM patterns.
        // 2. safe UI updates via binding (framework marshals to UI thread).
        // 3. fewer memory lifecycle issues.
        // 4. automatic UI synchronization.
        // 5. easier to test.
        // InitializeBindings();
    }

    ~CustomButton()
    {
        Trace.WriteLine($"Instance:{instanceId} Destructor");
        diagnostics.CustomButtonConstructedCount--;
    }

    void OnLoaded(object? sender, EventArgs e)
    {
        Trace.WriteLine($"Instance:{instanceId} Loaded (Loaded:{++loadedCount} Unloaded:{unloadedCount} BindingContext:{BindingContext.GetHashCode()})");
        diagnostics.CustomButtonLoadedCount++;

        // Defend against duplicate subscriptions (Hot Reload, reattach).
        // Event handlers may run on background threads.
        // Your own event handlers may not be thread safe.
        // Any direct UI access or collection updates must be marshalled to the UI thread, typically requires the Dispatcher.
        // Must be idempotent (safe to call multiple times).
        // UnsubscribeEvents();
        // SubscribeEvents();
    }

    void OnUnloaded(object? sender, EventArgs e)
    {
        Trace.WriteLine($"Instance:{instanceId} Unloaded (Loaded:{loadedCount} Unloaded:{++unloadedCount})");
        diagnostics.CustomButtonLoadedCount--;

        // Unsubscribe from events to prevent memory leaks and unintended behavior.
        // UnsubscribeEvents();
    }
}
