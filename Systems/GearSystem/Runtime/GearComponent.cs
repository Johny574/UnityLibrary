using System;
using UnityEngine;

[Serializable]
public class GearComponent 
{
    Gear _slots = new();
    public Action<Gearslot, GearItemSO> ItemEquiped, ItemUnequiped;

    public GearComponent(Gear slots)
    {
        _slots = slots;
    }

    public void Unequipt(GearItemSO.Slot _slot) {
        Gearslot slot = _slots[_slot];

        if (slot.Item == null)
            return;
        
        ItemUnequiped?.Invoke(slot, slot.Item);
        slot.Item = null;

        if (slot.Object != null)
            slot.Object.gameObject.SetActive(false);
    }

    public void Equipt(GearItemSO item) {
        if (item == null)
            return;

        Gearslot slot = _slots[item._Slot];
        slot.Item = item;
        ItemEquiped?.Invoke(slot, item);
        if (slot.Object == null)
            return;

        slot.Object.GetComponent<SpriteRenderer>().sprite = item.Icon;
        slot.Object.gameObject.SetActive(true);
    }

    #if UNITY_INCLUDE_TESTS
    public Gear Slots => _slots;
    #endif
}