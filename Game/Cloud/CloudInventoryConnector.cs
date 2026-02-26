using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;

public abstract class CloudInventoryConnector<T> : CloudConnector<List<T>> where T : ItemCloudSave {

    public override async Task<List<T>> LoadCloud() {

        List<PlayersInventoryItem> items = (await EconomyService.Instance.PlayerInventory.GetInventoryAsync()).PlayersInventoryItems;
        List<T> result = new();

        // no items in cloud
        if (items.Count <= 0)
            return new();

        foreach (var item in items) {
            try {
                ItemCloudSave saveData = JsonConvert.DeserializeObject<Dictionary<string, ItemSave>>(item.InstanceData.GetAsString(), Serializer.Settings)["Save"];
                if (saveData != null && saveData is T) {
                    result.Add((T)saveData);
                }
            }
            catch(NullReferenceException) {
            }
        }

        return result;
    }

    public override async void SaveCloud(List<T> save) {
        if (save == null)
            return;
        foreach (var item in save)
            await SaveItem(item); 
    }

    public override void OnDisable()
    {
        base.OnDisable();
        Save();
    }
}