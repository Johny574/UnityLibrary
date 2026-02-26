using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;

public static class EconomyManagementService 
{
    public static async Task<List<PlayerBalance>> GetBalances(int itemsperfetch) {
        GetBalancesOptions options = new GetBalancesOptions {
            ItemsPerFetch = itemsperfetch
        };

        List<PlayerBalance> result = (await EconomyService.Instance.PlayerBalances.GetBalancesAsync(options)).Balances;
        return result;
    }

    // public async void SetBalance(string currencyID, int newAmount)
    // {
    //     PlayerBalance newBalance = await EconomyService.Instance.PlayerBalances.SetBalanceAsync(currencyID, newAmount);
    // }

    // async void IncrementBalance()
    // {
    //     string currencyID = "GOLD_BARS";
    //     string writeLock = "someLockValueFromPreviousRequest";
    //     IncrementBalanceOptions options = new IncrementBalanceOptions
    //     {
    //         WriteLock = writeLock
    //     };

    //     PlayerBalance newBalance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyID, 1);
    //     // OR
    //     PlayerBalance otherNewBalance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyID, 1, options);
    // }

    // async void DecrementBalance()
    // {
    //     string currencyID = "GOLD_BARS";
    //     string writeLock = "someLockValueFromPreviousRequest";
    //     DecrementBalanceOptions options = new DecrementBalanceOptions
    //     {
    //         WriteLock = writeLock
    //     };

    //     PlayerBalance newBalance = await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(currencyID, 1);
    //     // OR
    //     PlayerBalance otherNewBalance = await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(currencyID, 1, options);
    // }
}
