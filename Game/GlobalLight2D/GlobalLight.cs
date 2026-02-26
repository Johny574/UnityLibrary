using UnityEngine;

public class GlobalLight2D : Singleton<GlobalLight2D> 
{
    [field: SerializeField] public Transform GlobalLightSource { get; private set; }
    [SerializeField] float _lightDistance = 50, _cycleDuration = 1440, _cycleTimer;
    [SerializeField] float _startAngle = 0, _stopAngle = 360, _currentAngle = 0;
    [SerializeField] Vector2 _pivot;

    void Update() {
        if (_cycleTimer < _cycleDuration)
            _cycleTimer += Time.deltaTime;
        else {
            _cycleTimer = 0f;
            _currentAngle = 0f;
        }

        _currentAngle = Mathf.Lerp(_startAngle, _stopAngle, _cycleTimer / _cycleDuration);
        Vector2 position = new Vector2(Mathf.Cos(_currentAngle * Mathf.Deg2Rad), Mathf.Sin(_currentAngle * Mathf.Deg2Rad));
        GlobalLightSource.transform.position = _pivot + position * _lightDistance;
    }
}