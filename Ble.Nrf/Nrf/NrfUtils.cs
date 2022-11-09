using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ble.Nrf.Nrf;

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
            logger?.LogError("Received unknown error code 0x{ErrorCode:X} ({NrfError})", error, NrfError.GetName(error));
            return true;
        }
        logger?.LogWarning("{Message}. Error code 0x{ErrorCode:X} ({NrfError})", message, error, NrfError.GetName(error));
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
            throw new Exception($"Received unknown error code 0x{error:X} ({NrfError.GetName(error)})");
        }
        throw new Exception($"{message}. Error code 0x{error:X} ({NrfError.GetName(error)})");
    }

    public static Task<bool> WithoutThrowing(this Task task)
    {
        Task<bool> continuationTask = task.ContinueWith(t => !t.IsFaulted);
        return continuationTask;
    }

    public static async Task<T?> NextAsync<T>(this IAsyncEnumerator<T> enumerator, CancellationToken token)
    {
        if (await enumerator.MoveNextAsync(token))
            return enumerator.Current;
        return default;
    }
}

public class EventSubscription<T>
{
    private readonly Queue<T> _receivedQueue;
    private EventSubscription()
    {
        _receivedQueue = new Queue<T>();
    }

    public static Action<T> Create(out IAsyncEnumerator<T> enumerator, CancellationToken cancellationToken)
    {
        var subscription = new EventSubscription<T>();
        enumerator = subscription.EnumerateAsync(cancellationToken).GetAsyncEnumerator(cancellationToken);
        return obj =>
        {
            subscription._receivedQueue.Enqueue(obj);
        };
    }

    public async IAsyncEnumerable<T> EnumerateAsync([EnumeratorCancellation] CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (_receivedQueue.Count > 0 && _receivedQueue.TryDequeue(out T? value))
                yield return value;
            await Task.Delay(10, token).WithoutThrowing();
        }
    }
}