using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Darp.DAQmx;

public class DaqMxException : Exception
{
    public DaqMxException(string message)
        : base(message)
    {
    }

    public DaqMxException(int errorCode, string message)
        : base($"{errorCode} - {message} ({DaqMx.GetErrorString(errorCode)})")
    {
    }

    public static void ThrowIfFailed(int status, [CallerMemberName] string? name = null)
    {
        if (status < 0)
            throw new DaqMxException(status, $"{name} failed");
    }

    public static T ThrowIfFailedOrReturn<T>(int status, T value, [CallerMemberName] string? name = null)
    {
        ThrowIfFailed(status, name);
        return value;
    }

    public delegate int BufferFunc<T>(in T pointer, uint length);
    public static string ThrowIfFailedOrReturnString(BufferFunc<byte> func, [CallerMemberName] string? name = null)
    {
        Span<byte> byteBuffer = stackalloc byte[1024];
        ThrowIfFailed(func(byteBuffer.GetPinnableReference(), (uint)byteBuffer.Length), name);
        return Encoding.ASCII.GetString(byteBuffer[..byteBuffer.IndexOf((byte)0)]);
    }

    public static IReadOnlyList<T> ThrowIfFailedOrReturnListOf<T>(BufferFunc<T> func, [CallerMemberName] string? name = null)
        where T : struct
    {
        var array = new T[1024];
        Span<T> byteBuffer = array;
        ThrowIfFailed(func(byteBuffer.GetPinnableReference(), (uint)byteBuffer.Length), name);
        return array
            .TakeWhile(x => !x.Equals(default(T)))
            .ToArray();
    }

    public delegate int Test<T>(out T outT);
    public static Lazy<T> LazyThrowIfFailedOrReturn<T>(Test<T> getter, [CallerMemberName] string? name = null) =>
        new(() => ThrowIfFailedOrReturn(getter(out T value), value, name));
    public static Lazy<TResult> LazyThrowIfFailedOrReturn<T, TResult>(Test<T> getter,
        Func<T, TResult> selector,
        [CallerMemberName] string? name = null) =>
        new(() => ThrowIfFailedOrReturn(getter(out T value), selector(value), name));

    public static Lazy<string> LazyThrowIfFailedOrReturnString(BufferFunc<byte> func,
        [CallerMemberName] string? name = null) => new(() => ThrowIfFailedOrReturnString(func, name));
    public static Lazy<TResult> LazyThrowIfFailedOrReturnString<TResult>(BufferFunc<byte> func,
        Func<string, TResult> selector,
        [CallerMemberName] string? name = null) => new(() => selector(ThrowIfFailedOrReturnString(func, name)));
    public static Lazy<IReadOnlyList<T>> LazyThrowIfFailedOrReturnListOf<T>(BufferFunc<T> func,
        [CallerMemberName] string? name = null) where T : struct => new(() => ThrowIfFailedOrReturnListOf(func, name));
    public static Lazy<IReadOnlyList<TResult>> LazyThrowIfFailedOrReturnListOf<T, TResult>(BufferFunc<T> func,
        Func<T, TResult> selector,
        [CallerMemberName] string? name = null) where T : struct =>
        new(() => ThrowIfFailedOrReturnListOf(func, name).Select(selector).ToArray());
}