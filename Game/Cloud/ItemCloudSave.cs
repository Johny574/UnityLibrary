
using System;

[Serializable]
public abstract class ItemCloudSave
{
    public string PlayersInventoryItemID = "";

    public ItemCloudSave(string playersInventoryItemID) {
        PlayersInventoryItemID = playersInventoryItemID;
    }
}