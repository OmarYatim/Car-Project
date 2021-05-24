using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float MotorForce;
    private float MaxSpeed;

    [SerializeField] WheelCollider FrontDriverW, FrontPassengerW;
    [SerializeField] WheelCollider BackDriverW, BackPassengerW;
    [SerializeField] List<WheelCollider> DrivingWheels;
    [SerializeField] List<WheelCollider> SteeringWheels;
    [SerializeField] Transform FrontDriverT, FrontPassengerT;
    [SerializeField] Transform BackDriverT, BackPassengerT;
    [SerializeField] Transform CenterofMass;
    [SerializeField] private float MaxSteerAngle = 30;
    [SerializeField] private float MaxForwardSpeed = 120;
    [SerializeField] private float MaxReversSpeed = 20;
    [SerializeField] private float BraqueStrength = 200;

    [HideInInspector] public double speed;
    [HideInInspector] public float DefaultMotorForce = 200;

    private Rigidbody rb;
    private AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        if(CenterofMass)
        {
            rb.centerOfMass = CenterofMass.localPosition;
        }

        MotorForce = DefaultMotorForce;
    }
    public void GetInput()
    {
        horizontalInput = InputManager.Instance.GetHorizontalInput();
        verticalInput = InputManager.Instance.GetVerticalInput();
    }

    private void Steere()
    {
        foreach(WheelCollider wheel in SteeringWheels)
        {
            steeringAngle = MaxSteerAngle * horizontalInput;
            wheel.steerAngle = steeringAngle;
        }
    }

    private void Accelerate()
    {
        if(!InputManager.Instance.GetBrakeInput())
            foreach(WheelCollider wheel in DrivingWheels)
            {
                wheel.motorTorque = verticalInput * MotorForce;
                wheel.brakeTorque = 0;
            }
    }

    private void UpdateWheePoses()
    {
        UpdateWheelPose(FrontDriverW, FrontDriverT);
        UpdateWheelPose(FrontPassengerW, FrontPassengerT);
        UpdateWheelPose(BackDriverW, BackDriverT);
        UpdateWheelPose(BackPassengerW, BackPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 pos = _transform.position;
        Quaternion rot = _transform.rotation;

        _collider.GetWorldPose(out pos, out rot);

        _transform.position = pos;
        _transform.rotation = rot;
    }

    void Brake()
    {
        if (InputManager.Instance.GetBrakeInput())
        {
            foreach(WheelCollider wheel in DrivingWheels)
            {
                wheel.motorTorque = 0;
                wheel.brakeTorque = BraqueStrength;
            }
        }
    }

    void SpeedCalculations()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6;
        Debug.Log(speed);
        Vector3 velocity = rb.velocity;
        Vector3 localvelocity = transform.InverseTransformDirection(velocity);
        MaxSpeed = localvelocity.z > 0 ? MaxForwardSpeed : MaxReversSpeed;
        MotorForce = speed < MaxSpeed ? DefaultMotorForce : 0;   
    }

    private void FixedUpdate()
    {
        GetInput();
        Brake();
        Steere();
        Accelerate();
        UpdateWheePoses();
        SpeedCalculations();
        if(GameManager.Instance.state == GameState.EngineStarted)
            SoundManager.Instance.PlayEngineSound(source, speed, MaxForwardSpeed);
    }
}
