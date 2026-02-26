using System;
using UnityEngine;
public class ManaComponent 
{
    public float Mana;

    public ManaComponent(ManaBehaviour behaviour) {
    }

    // public void Initilize() {
    //     _stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
    //     Data = new (_stats.ManaPool, _stats.ManaPool);
    //     Changed?.Invoke(Data, 0);
    // }

    public void Tick() {
        // if (Data.Amount < _stats.ManaPool) {
        //     Update(_stats.ManaRegen * Time.deltaTime);
        // }
    }

    public void Update(float amount) {
        Mana += amount;
        // Data.Amount += amount;
        // Data.Amount = Mathf.Clamp(Data.Amount, 0, _stats.ManaPool);
        // Data.CalculateFill();
        // Changed?.Invoke(Data, 0);
    }

    // public BarData Save() {
    //     return Data;
    // }

    // public void Load(BarData save) {
    //     Data = save;
    //     Changed?.Invoke(Data, 0);
    // }
}