


using UnityEngine;

public class Follower : MonoBehaviour {
    [SerializeField] float _radius = 5, _speed = .1f;
    private GameObject _following;
    [SerializeField] Vector2 _offset = Vector2.zero;
    Vector2 _followpoint;

    void Start()
    {
        if (_following != null)
        {
            return;
        }
        _following = GameObject.FindGameObjectWithTag("Player");
    }

    public void Follow(GameObject gameObject) => _following = gameObject;

    void Update() {
        _followpoint = (Vector2)_following.transform.position + _offset;
        
        if (_speed.Equals(0f)){
            transform.position = _followpoint;
        }

        if (Vector2.Distance(transform.position, _followpoint) < _radius) {
            var dif = (_followpoint - (Vector2)transform.position).normalized;
            transform.position = (Vector2)transform.position + dif * _speed;
        }
    }

}