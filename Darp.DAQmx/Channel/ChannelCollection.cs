using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel;

public class ChannelCollection<TTask, TChannel> : IList<TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    private readonly IList<TChannel> _channels;
    public ChannelCollection(TTask task)
    {
        Task = task;
        _channels = new List<TChannel>();
    }

    public TTask Task { get; }
    public IChannelReader<TTask, TChannel> GetReader() => ChannelCount > 1
        ? new ChannelReader<TTask, TChannel>(Task)
        : new SingleChannelReader<TTask, TChannel>(Task);

    public ISingleChannelReader<TTask, TChannel> GetSingleReader() => new SingleChannelReader<TTask, TChannel>(Task);
    public IEnumerator<TChannel> GetEnumerator() => _channels.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void Add(TChannel item) => _channels.Add(item);
    public void Clear() => _channels.Clear();
    public bool Contains(TChannel item) => _channels.Contains(item);
    public void CopyTo(TChannel[] array, int arrayIndex) => _channels.CopyTo(array, arrayIndex);
    public bool Remove(TChannel item) => _channels.Remove(item);
    public int Count => _channels.Count;
    public bool IsReadOnly => _channels.IsReadOnly;
    public int IndexOf(TChannel item) => _channels.IndexOf(item);
    public void Insert(int index, TChannel item) => _channels.Insert(index, item);
    public void RemoveAt(int index) => _channels.RemoveAt(index);
    public TChannel this[int index] { get => _channels[index]; set => _channels[index] = value; }
    public int ChannelCount => _channels.Sum(x => x.NumberOfVirtualChannels);
}