


using UnityEngine;


public class ManaBehaviour : MonoBehaviour
{
    public ManaComponent Mana;
    void Awake() {
        Mana = new(this);
    }

    void Update() {
        Mana.Tick();
    }
}