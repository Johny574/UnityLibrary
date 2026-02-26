using UnityEngine;

public class PlayerCharacterController3D : MonoBehaviour
{
    float _xLookRotation = 0f;
    float _yLookRotation = 0f;    
    [SerializeField] Vector2 _lookSensitivity = Vector2.one;
    [SerializeField] Camera _lookCamera;
    [SerializeField] private float _yLookClampMin = -35f , _yLookClampMax = 35f;
    private Vector2 _frameInput = Vector2.zero;
    
    protected void FixedUpdate()
    {
        Move(new Vector3(_frameInput.x, 0f, _frameInput.y));
    }

    void Update()
    {
        _frameInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _xLookRotation += Input.GetAxis("Mouse X") * _lookSensitivity.x;
        _yLookRotation -= Input.GetAxis("Mouse Y") * _lookSensitivity.y;
        _yLookRotation = Mathf.Clamp(_yLookRotation, _yLookClampMin, _yLookClampMax);
        transform.rotation = Quaternion.Euler(0, _xLookRotation, 0f);
        _lookCamera.transform.localRotation = Quaternion.Euler(_yLookRotation, 0f, 0f);
    }

    [SerializeField] protected float _moveSpeed = 2f;

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    } 
}
