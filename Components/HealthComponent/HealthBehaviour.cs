


using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public HealthComponent Health { get; set; }
    
    [SerializeField] float _health = 100;

    void Awake() {
        Health = new();
    }

    void Start() {
        Health.Initilize(_health);
    }

    public void Die() => Health.Die();

    #if UNITY_EDITOR
    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            Health.Change(-1);
        }
    }
    #endif

}