using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Services.Authentication;
using Unity.Services.Economy;
using UnityEngine;

public abstract class CloudConnector<S> : LocalConnector<S> {
    public ICloudAdapter<S> Adapter;
    public abstract void SaveCloud(S save);
    public abstract Task<S> LoadCloud();

    public void Create(ICloudAdapter<S> adapter) {
        Adapter = adapter;
        Adapter.Init(this);
        Authenticator.FinishedLoading += () => Initilize();
    }

    public virtual void OnDisable()
    {
        Authenticator.FinishedLoading -= () => Initilize();
    }

    public override async void Initilize()   {
        var save = await InitializeAsync();
        Adapter?.Load(save);
    }

    public async Task<S> InitializeAsync() {
        S save = LoadLocal();

        if (save == null)
            save = await LoadCloud();   
        else {
            try  {
                SaveCloud(save);
                File.Delete(Path.Join(Serializer.AppRelativeSavePath, LocalSave));
            }
            catch( CloudException ) {
                Debug.Log("There was a error syncing the data to the cloud");
            }
        }
        return save;
    }

    public void Save() {
        if (!AuthenticationService.Instance.IsSignedIn)
            return;

        var save = Adapter.Save();
        if (save == null)
            return;

        if (Application.internetReachability == NetworkReachability.NotReachable)
            SaveLocal(save); // fallback - local
        else
            SaveCloud(save); // normal path
    }


    protected virtual async Task SaveItem(ItemCloudSave  saveData) {
        // keep this wrapper for type safety
        var save = new Dictionary<string, object> { { "Save", saveData }
        };

        // Serialize with type info
        string instanceJson = JsonConvert.SerializeObject(save, Serializer.Settings);

        // Deserialize into JObject (preserves $type correctly)
        var instanceData = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(instanceJson);

        // sync
        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(saveData.PlayersInventoryItemID, instanceData);
    }
}

public class CloudException : Exception {

}