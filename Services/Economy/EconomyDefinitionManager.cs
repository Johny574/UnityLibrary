using System.Collections.Generic;
using System.Linq;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;

public class EconomyDefinitions : Singleton<EconomyDefinitions> {
    public Dictionary<string, InventoryItemDefinition> ItemDefinitions;
    public Dictionary<string, Unity.Services.Economy.Model.CurrencyDefinition> CurrencyDefintions;
    public Dictionary<string, VirtualPurchaseDefinition> PurchaseDefinitions;
    void OnEnable() => Authenticator.FinishedLoading += FinishedSignIn;
    void OnDisable() => Authenticator.FinishedLoading -= FinishedSignIn;
    private void FinishedSignIn() {
        ItemDefinitions = EconomyService.Instance.Configuration.GetInventoryItems().ToDictionary(x => x.Id, x => x);
        CurrencyDefintions = EconomyService.Instance.Configuration.GetCurrencies().ToDictionary(x => x.Id, x => x);
        PurchaseDefinitions = EconomyService.Instance.Configuration.GetVirtualPurchases().ToDictionary(x => x.Id, x =>x);
    }
}
