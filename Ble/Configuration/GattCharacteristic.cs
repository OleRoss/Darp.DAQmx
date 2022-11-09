using System;
using System.Collections.Generic;
using Ble.Connection;
using Ble.Utils;

namespace Ble.Configuration;

public interface IGattCharacteristic<out TPropOne>
{
    TPropOne PropertyOne { get; }
}

public abstract class GattCharacteristic
{
    protected readonly List<GattCharacteristic> _descriptors = new();
    public IReadOnlyList<GattCharacteristic> Descriptors => _descriptors;
    public abstract Guid Uuid { get; }
    public abstract Property Property { get; }

    public bool IsValid(IConnectedGattCharacteristic connChar)
    {
        if ((Property & connChar.Property) != Property)
            return false;
        return AreDescriptorsValid(connChar);
    }

    protected abstract bool AreDescriptorsValid(IConnectedGattCharacteristic connChar);
}

public class GattCharacteristic<TPropOne> : GattCharacteristic, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
{
    public GattCharacteristic(Guid characteristicUuid) => Uuid = characteristicUuid;
    public GattCharacteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }

    public override Guid Uuid { get; }
    public override Property Property => PropertyOne.Property;

    public TPropOne PropertyOne => new();
    
    protected override bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return PropertyOne.AreDescriptorsValid(connChar);
    }
}

public class GattCharacteristic<TPropOne, TPropTwo> : GattCharacteristic<TPropTwo>, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
    where TPropTwo : IGattProperty, new()
{
    public GattCharacteristic(Guid characteristicUuid) : base(characteristicUuid) { }
    public GattCharacteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }

    public new TPropOne PropertyOne => new();
    public TPropTwo PropertyTwo => new();
    public override Property Property => PropertyOne.Property | PropertyTwo.Property;
    protected override bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return PropertyOne.AreDescriptorsValid(connChar) && PropertyTwo.AreDescriptorsValid(connChar);
    }
}

public class GattCharacteristic<TPropOne, TPropTwo, TPropThree> : GattCharacteristic<TPropTwo, TPropThree>, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
    where TPropTwo : IGattProperty, new()
    where TPropThree : IGattProperty, new()
{
    public GattCharacteristic(Guid characteristicUuid) : base(characteristicUuid) { }
    public GattCharacteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }
 
    public new TPropOne PropertyOne => new();
    public new TPropTwo PropertyTwo => new();
    public TPropTwo PropertyThree => new();
    public override Property Property => PropertyOne.Property | PropertyTwo.Property | PropertyThree.Property;

    protected override bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return PropertyOne.AreDescriptorsValid(connChar)
               && PropertyTwo.AreDescriptorsValid(connChar)
               && PropertyThree.AreDescriptorsValid(connChar);
    }
}