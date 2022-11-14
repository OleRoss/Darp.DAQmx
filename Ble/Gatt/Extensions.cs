using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ble.Config;
using Ble.Gap;

namespace Ble.Gatt;

public abstract class AbstractObserver<TObserver, TObservable> : IObserver<TObserver>, IObservable<TObservable>
{
    private readonly List<IObserver<TObservable>> _observers = new();
    public void OnCompleted()
    {
        foreach (IObserver<TObservable> observer in _observers) observer.OnCompleted();
    }

    public void OnError(Exception error)
    {
        foreach (IObserver<TObservable> observer in _observers) observer.OnError(error);
    }

    public void OnNext(TObserver value)
    {
        if (!_observers.Any())
            return;
        if (!Next(value, out TObservable? outValue))
            return;
        foreach (IObserver<TObservable> observer in _observers) observer.OnNext(outValue);
    }

    protected abstract bool Next(TObserver value, [NotNullWhen(true)] out TObservable? outValue);

    public IDisposable Subscribe(IObserver<TObservable> observer) => new Subscription<TObservable>(_observers, observer);
}
public sealed class Subscription<TObservable> : IDisposable
{
    private readonly ICollection<IObserver<TObservable>> _observers;
    private readonly IObserver<TObservable> _observer;

    public Subscription(ICollection<IObserver<TObservable>> observers, IObserver<TObservable> observer)
    {
        _observers = observers;
        _observer = observer;
        _observers.Add(observer);
    }

    public void Dispose() => _observers.Remove(_observer);
}

public sealed class ConfigurationObserver : AbstractObserver<IGapAdvertisement, IConnectedPeripheral>
{
    protected override bool Next(IGapAdvertisement value, [NotNullWhen(true)] out IConnectedPeripheral? outValue)
    {
        throw new NotImplementedException();
    }
}

public sealed class WhereConfigurationObserver : AbstractObserver<IGapAdvertisement, IGapAdvertisement>
{
    private readonly Configuration _configuration;

    public WhereConfigurationObserver(Configuration configuration) => _configuration = configuration;

    protected override bool Next(IGapAdvertisement adv, [NotNullWhen(true)] out IGapAdvertisement? outValue)
    {
        outValue = adv;
        if (_configuration.Advertisement.Services.Count == 0)
            return true;
        if (adv.Services.Length == 0)
            return false;
        bool res = _configuration.Advertisement.Services.All(advertisementService =>
            adv.Services.Contains(advertisementService));
        return res;
    }
}