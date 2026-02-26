using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearItemSO", menuName = "Items/Items/GearItemSO", order = 1)]
public class GearItemSO : ItemSO 
{
    public Slot _Slot { get; private set; }
    public enum Slot {
        Primary,
        Secondary,
        Necklace,
        Boots,
        Helm,
        Ring1,
        Ring2,
        Torso,
        Pants
    }
    #if UNITY_INCLUDE_TESTS
    public void SetSlot(Slot slot) => _Slot = slot;
    #endif
}