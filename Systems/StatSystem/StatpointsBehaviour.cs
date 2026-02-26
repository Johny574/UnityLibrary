


using UnityEngine;


public class StatpointsBehaviour : MonoBehaviour
{
    public StatpointsComponent Stats { get; set; }
    [SerializeField] StatPoints defualtStats;

    void Awake() {
        Stats = new(this, defualtStats);
    }
}