
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    float _rotX = 0f;
    [SerializeField] float _rotationSpeed = 5, _movementSpeed = 1;
    private GameObject _localPlayer;

    [Range(0, 89.9f)]
    [SerializeField] float _cameraAngle = 55f;

    [SerializeField] float _zoom = 10f, _zoomMin = 5f, _zoomMax = 15f;
    [SerializeField] bool _playerLocked = true;
    Vector3 _focusPoint;

    void Awake() {
        _localPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        // rotation
        _rotX += (Input.GetKey(KeyCode.Q) ? 1 : (Input.GetKey(KeyCode.E) ? -1 : 0)) * _rotationSpeed;

        // zoom
        _zoom -= Input.mouseScrollDelta.y;
        _zoom = Mathf.Clamp(_zoom, _zoomMin, _zoomMax);

        if (_playerLocked) {
            _focusPoint = _localPlayer.transform.position;
        }
        else {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 direction = transform.TransformDirection(input);
            direction.y = 0;
            direction.Normalize();
            _focusPoint += direction * _movementSpeed;
        }

        float radElevation = _cameraAngle * Mathf.Deg2Rad;  // up/down
        float radAzimuth = _rotX * Mathf.Deg2Rad;           // left/right

        // Convert spherical to Cartesian
        Vector3 offset = new Vector3(
            Mathf.Cos(radElevation) * Mathf.Sin(radAzimuth),
            Mathf.Sin(radElevation),
            Mathf.Cos(radElevation) * Mathf.Cos(radAzimuth)
        ) * _zoom;

        transform.position = _focusPoint + offset;
        transform.LookAt(_focusPoint);
    }
}
