using System.Collections;
using System.Collections.Generic;

namespace Ble.Config;

public sealed class GenericCollection<TValue, TParent> : ICollection<TValue>
{
    private readonly List<TValue> _values = new();

    public GenericCollection(TParent parent) => Parent = parent;

    public TParent Parent { get; }
    public int Count => _values.Count;
    public bool IsReadOnly => false;

    public TParent Add(TValue item)
    {
        _values.Add(item);
        return Parent;
    }
    void ICollection<TValue>.Add(TValue item) => _values.Add(item);
    public void Clear() => _values.Clear();
    public bool Contains(TValue item) => _values.Contains(item);
    public void CopyTo(TValue[] array, int arrayIndex) => _values.CopyTo(array, arrayIndex);
    public bool Remove(TValue item) => _values.Remove(item);

    public IEnumerator<TValue> GetEnumerator() => _values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}