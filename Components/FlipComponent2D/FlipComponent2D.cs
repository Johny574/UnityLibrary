using UnityEngine;
using UnityEngine.AI;

public class FlipComponent2D : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer;
    NavMeshAgent _agent;
    bool _invert = false;
    public bool Flipped { get; set; } = false;

    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Flip(bool flip) {
        Flipped = flip;
        _renderer.flipX =  _invert ? flip : !flip; 
        // foreach (var key in _gear.Gear.Keys)
        //     if (_gear.Gear[key].Object != null && _gear.Gear[key].Item != null && _gear.Gear[key].Renderer != null)
        //         _gear.Gear[key].Renderer.flipX = !flip;
    }
}