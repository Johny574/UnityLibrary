


using UnityEngine;


public class InventoryBehaviour : MonoBehaviour
{
    public InventoryComponent Inventory { get; set; }
    public ItemStack[] Items = new ItemStack[16];

    void Awake() {
        Inventory = new InventoryComponent(this, Items);
    }
}