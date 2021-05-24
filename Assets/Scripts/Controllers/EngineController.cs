using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController), typeof(AudioSource))]
public class EngineController : MonoBehaviour
{
    [SerializeField] private float MotorForce = 200;

    private AudioSource source;
    private CarController ControllerScript;

    private void Start()
    {
        ControllerScript = GetComponent<CarController>();
        source = GetComponent<AudioSource>();
        ControllerScript.DefaultMotorForce = 0;
    }

    void StartEngine()
    {
        StartCoroutine(SoundManager.Instance.PlayStartSound(source));
        Debug.Log("kmel");
        ControllerScript.DefaultMotorForce = MotorForce;
    }

    void StopEngine()
    {
        ControllerScript.DefaultMotorForce = 0;
        source.clip = null;
        GameManager.Instance.state = GameState.EngineStopped;
    }

    private void Update()
    {
        if(InputManager.Instance.GetEngineInput())
        {
            if (GameManager.Instance.state == GameState.EngineStarted)
                StopEngine();
            else
                StartEngine();
        }
    }
}
