using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] WheelColliders _wheelColliders;
    [SerializeField] WheelMeshes _wheelMeshes;
    float _throttle, _steering, _brake;
    [SerializeField] AnimationCurve _steeringCurve;
    float _slipAngle;
    Rigidbody _rb;
    [SerializeField] float _motorpower = 100;
    [SerializeField] float _brakePower;
    float _rbSpeed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _rbSpeed = _rb.linearVelocity.magnitude;
        _slipAngle = Vector3.SignedAngle(transform.forward, _rb.linearVelocity-transform.forward, Vector3.up);

        // if (_slipAngle < 120f)
        // {
        //     if (_throttle < 0)
        //     {
        //         _brake = Mathf.Abs(_throttle);
        //         _throttle = 0;
        //     }
        //     else
        //     {
        //         _brake = 0;
        //     }
        // }
        // else
        // {
        //     _brake = 0;
        // }

        GetInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        UpdateWheels();
    }

    void GetInput()
    {
        _throttle = Input.GetAxis("Vertical");
        _steering = Input.GetAxis("Horizontal");
    }

    void UpdateWheels()
    {
        UpdateWheel(_wheelColliders._FLWheel, _wheelMeshes._FLWheel);
        UpdateWheel(_wheelColliders._FRWheel, _wheelMeshes._FRWheel);
        UpdateWheel(_wheelColliders._RRWheel, _wheelMeshes._RRWheel);
        UpdateWheel(_wheelColliders._RLWheel, _wheelMeshes._RLWheel);
    }

    void ApplyBrake()
    {
        _wheelColliders._FRWheel.brakeTorque = _brake * _brakePower * .7f;
        _wheelColliders._FLWheel.brakeTorque = _brake * _brakePower * .7f;

        _wheelColliders._RRWheel.brakeTorque = _brake * _brakePower * .3f;
        _wheelColliders._RLWheel.brakeTorque = _brake * _brakePower * .3f;
    }

    void ApplyMotor()
    {
        _wheelColliders._RLWheel.motorTorque = _motorpower * _throttle;
        _wheelColliders._RRWheel.motorTorque = _motorpower * _throttle;
    }

    void ApplySteering()
    {
        float steeringangle = _steering * _steeringCurve.Evaluate(_rbSpeed);

        if (_slipAngle < 120f)
        {
            steeringangle += Vector3.SignedAngle(transform.forward, _rb.linearVelocity+ transform.forward, Vector3.up);
        }
        steeringangle = Mathf.Clamp(steeringangle, -90f, 90f);
        _wheelColliders._FLWheel.steerAngle = steeringangle;
        _wheelColliders._FRWheel.steerAngle = steeringangle;
    }

    void UpdateWheel(WheelCollider collider, MeshRenderer mesh)
    {
        Quaternion rot;
        Vector3 pos;
        collider.GetWorldPose(out pos, out rot);
        mesh.transform.position = pos;
        mesh.transform.rotation = rot;
    }
}


[Serializable]
public class WheelColliders
{
    public WheelCollider _RRWheel;
    public WheelCollider _RLWheel;
    public WheelCollider _FRWheel;
    public WheelCollider _FLWheel;
}

[Serializable]
public class WheelMeshes
{
    public MeshRenderer _RRWheel;
    public MeshRenderer _RLWheel;
    public MeshRenderer _FRWheel;
    public MeshRenderer _FLWheel;
}