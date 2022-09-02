﻿using System.Collections;
using System.Collections.Generic;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel;

public class ChannelCollection<TTask, TChannel> : ICollection<TChannel>
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

    public IEnumerator<TChannel> GetEnumerator() => _channels.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(TChannel item) => _channels.Add(item);

    public void Clear() => _channels.Clear();

    public bool Contains(TChannel item) => _channels.Contains(item);

    public void CopyTo(TChannel[] array, int arrayIndex) => _channels.CopyTo(array, arrayIndex);

    public bool Remove(TChannel item) => _channels.Remove(item);

    public int Count => _channels.Count;
    public bool IsReadOnly => _channels.IsReadOnly;
}