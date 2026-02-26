
using System;
using UnityEngine;

[Serializable]
public class Gearslot
{
    public GearItemSO Item;
    public GameObject Object;

    public Gearslot(GearItemSO data, GameObject @object) {
        Item = data;
        Object = @object;
    }
}