using Microsoft.JSInterop;
using System.Diagnostics;

namespace PlayerJs.Pages;

public partial class PlayerjsTestPage : IDisposable
{
    private bool _disposed;
    private string _log = string.Empty;
    private DotNetObjectReference<PlayerjsTestPage> _dotnetObj = default!;



    protected override Task OnInitAsync()
    {
        _dotnetObj = DotNetObjectReference.Create(this);

        return base.OnInitAsync();
    }

    protected override async Task OnAfterFirstRenderAsync()
    {
        var t = JsRuntime.InvokeVoidAsync("BitPlayerJS.init", _dotnetObj, "playerjs-player", "https://demo.unified-streaming.com/k8s/features/stable/video/tears-of-steel/tears-of-steel.ism/.m3u8");
        try
        {
            await t;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        Debug.WriteLine(t.IsFaulted);
        Debug.WriteLine(t.IsCompletedSuccessfully);
    }



    [JSInvokable("PlayerjsEvents")]
    public void PlayerjsEvents(string e, string id, object data)
    {
        _log = $"[{DateTimeOffset.Now.ToString("HH:mm:ss")}] event: {e}, id: {id}, data:{data}\n{_log}";
        StateHasChanged();
    }



    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed || disposing is false) return;

        _ = JsRuntime.InvokeVoidAsync("BitPlayerJS.dispose");
    }
}
