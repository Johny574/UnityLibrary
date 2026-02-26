
using System;
using Unity.Services.Economy.Tools;
using UnityEngine.Purchasing;

[Serializable]
public class ProductDefinition
{
    public PurchasesHelper PurchasesHelper;
    public ProductType Type;
    public string GoogleProductID;
    public string ID;
    public ProductClass Class;

    public ProductDefinition(PurchasesHelper purchasesHelper, ProductType type, string googleProductID, string iD) {
        PurchasesHelper = purchasesHelper;
        Type = type;
        GoogleProductID = googleProductID;
        ID = iD;
    }

    public enum ProductCategory {
        Gold
    }
    public enum ProductClass {
        Currency,
        
    }
}