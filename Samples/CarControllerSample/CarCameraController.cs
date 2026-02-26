using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    [SerializeField] Rigidbody _player;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector3 _lookoffset;
    [SerializeField] float _speed;

    [SerializeField] float _five = -5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Vector3 playerForward = (_player.linearVelocity + _player.transform.forward).normalized;
        // transform.position = Vector3.Lerp(transform.position, _player.transform.position + _player.transform.TransformVector(_offset) + playerForward * (-5f), _speed * Time.deltaTime);
        // transform.LookAt(_player.transform.position);
         Vector3 playerForward = (_player.linearVelocity + _player.transform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position,
            _player.position + _player.transform.TransformVector(_offset)
            + playerForward * (_five),
            _speed * Time.deltaTime);
        transform.LookAt(_player.transform.position + _lookoffset);
    }
}
