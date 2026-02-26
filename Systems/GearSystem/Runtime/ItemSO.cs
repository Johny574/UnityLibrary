using System;
using UnityEngine;
// using UnityEngine.AddressableAssets;


[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Items/ItemData", order = 1)]
[Serializable]
public class ItemSO : ScriptableObject
{
    // public AssetReference Prefab { get; private set; }
    // #if UNITY_EDITOR
    //     void OnValidate()
    //     {
    //         if (ID != 0) return;
    //         ID = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
    //         UnityEditor.EditorUtility.SetDirty(this);
    //     }
    // #endif
    
    [Header("Info")]
    public string DisplayName { get; private set; }
    public string Description { get; private set; }
    public ItemGrade Grade { get; private set; }
    [field:SerializeField] public Sprite Icon  { get; private set; }

    [Header("Inventory")]
    public int StackLimit { get; private set; } = 16;
    public int ID { get; private set; }

    [Header("Price")]
    public int CostPrice { get; set; }
    public float SellPercentage = .7f;

}