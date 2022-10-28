using Serilog;

namespace NrfBleDriver;

public static class NrfUtils
{
    public static bool IsFailed(this uint error, ILogger? logger = null, Func<uint, string>? messageGetter = null) =>
        IsFailed(error, logger, messageGetter?.Invoke(error) ?? "");

    public static bool IsFailed(this uint error, ILogger? logger = null, string message = "")
    {
        if (error == NrfError.NRF_SUCCESS)
            return false;
        if (string.IsNullOrWhiteSpace(message))
        {
            logger?.Error("Received unknown error code 0x{ErrorCode:X}", error);
            return true;
        }
        logger?.Warning("{Message}. Error code 0x{ErrorCode:X}", message, error);
        return true;
    }

    public static bool IsFailed(this uint error, out uint outError, ILogger? logger = null, string message = "")
    {
        outError = error;
        return IsFailed(error, logger, message);
    }
    public static void ThrowIfFailed(this uint error, Func<uint, string>? messageGetter = null) =>
        ThrowIfFailed(error, messageGetter?.Invoke(error) ?? "");

    public static void ThrowIfFailed(this uint error, string message = "")
    {
        if (error == NrfError.NRF_SUCCESS)
            return;
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new Exception($"Received unknown error code 0x{error:X}");
        }
        throw new Exception(message);
    }

    public static async Task<T> FirstEventAsync<T>(Func<T> action, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(100, token);
        }
        return action();
    }

    public static Action<T> CreateEventSubscription<T>(out Func<T> func,
        ushort timeoutMs,
        out CancellationToken cancellationToken)
    {
        var source = new CancellationTokenSource();
        source.CancelAfter(timeoutMs);
        cancellationToken = source.Token;
        var value = default(T);
        func = () => value!;
        return obj =>
        {
            value = obj;
            source.Cancel();
        };
    }
}