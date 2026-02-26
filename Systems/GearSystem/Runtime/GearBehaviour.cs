


using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : MonoBehaviour {
    GearComponent _gear;
    [field: SerializeField] public Gear Slots = new()
    {
        { GearItemSO.Slot.Primary,   new Gearslot(null, null) },
        { GearItemSO.Slot.Secondary, new Gearslot(null, null) },
        { GearItemSO.Slot.Necklace,  new Gearslot(null, null) },
        { GearItemSO.Slot.Torso,     new Gearslot(null, null) },
        { GearItemSO.Slot.Helm,      new Gearslot(null, null) },
        { GearItemSO.Slot.Boots,     new Gearslot(null, null) },
        { GearItemSO.Slot.Ring1,     new Gearslot(null, null) },
        { GearItemSO.Slot.Ring2,     new Gearslot(null, null) },
    };

    void Awake() {
        _gear = new(Slots);
    }
    public void Unequipt(GearItemSO.Slot slot) {
        _gear.Unequipt(slot);
    }

   public void Equipt(GearItemSO item)
    {
        _gear.Equipt(item);
    }
}
