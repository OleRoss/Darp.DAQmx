using System;
using System.Collections.Generic;
using Ble.Gatt;
using Ble.Uuid;

namespace Ble.Config;

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

public class Characteristic<TPropOne> : GattCharacteristic, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
{
    public Characteristic(Guid characteristicUuid) => Uuid = characteristicUuid;
    public Characteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }

    public override Guid Uuid { get; }
    public override Property Property => PropertyOne.Property;

    public TPropOne PropertyOne => new();
    
    protected override bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return PropertyOne.AreDescriptorsValid(connChar);
    }
}

public class Characteristic<TPropOne, TPropTwo> : Characteristic<TPropTwo>, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
    where TPropTwo : IGattProperty, new()
{
    public Characteristic(Guid characteristicUuid) : base(characteristicUuid) { }
    public Characteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }

    public new TPropOne PropertyOne => new();
    public TPropTwo PropertyTwo => new();
    public override Property Property => PropertyOne.Property | PropertyTwo.Property;
    protected override bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return PropertyOne.AreDescriptorsValid(connChar) && PropertyTwo.AreDescriptorsValid(connChar);
    }
}

public class Characteristic<TPropOne, TPropTwo, TPropThree> : Characteristic<TPropTwo, TPropThree>, IGattCharacteristic<TPropOne>
    where TPropOne : IGattProperty, new()
    where TPropTwo : IGattProperty, new()
    where TPropThree : IGattProperty, new()
{
    public Characteristic(Guid characteristicUuid) : base(characteristicUuid) { }
    public Characteristic(string characteristicUuid) : this(characteristicUuid.ToBleGuid()) { }
 
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