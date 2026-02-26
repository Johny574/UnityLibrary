using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GearTests 
{
    GearComponent _gear;
    GearItemSO _mockItem;
    GearItemSO.Slot _slot;
    [SetUp]
    public void SetUp() {
        
        Gear Slots = new() {
            {GearItemSO.Slot.Primary , new Gearslot(null, null)},
        };

        _mockItem = ScriptableObject.CreateInstance<GearItemSO>();
        _slot = GearItemSO.Slot.Primary;
        _mockItem.SetSlot(_slot);
        _gear = new(Slots);
    }

    [Test]
    public void ItemRemoved() {
        Assert.IsNotNull(_mockItem);
        _gear.Equipt(_mockItem);
        Assert.IsNotNull(_gear.Slots[_slot]);
    }

    [Test]
    public void ItemAdded() {
        Assert.IsNotNull(_mockItem);
        _gear.Equipt(_mockItem);
        Assert.IsNotNull(_gear.Slots[_slot]);
        _gear.Unequipt(_slot);
        Assert.IsNull(_gear.Slots[_slot].Item);

    }
}
