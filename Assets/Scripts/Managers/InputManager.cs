using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputData InputAxes;
    [HideInInspector] public static InputManager Instance;


    private void Awake()
    {
        Instance = this;
    }
    public float GetHorizontalInput()
    {
        return Input.GetAxis(InputAxes.HorizontalInput);
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis(InputAxes.VerticalInput);
    }

    public bool GetBrakeInput()
    {
        return Input.GetButton(InputAxes.BrakeInput);
    }

    public bool GetHornInput()
    {
        return Input.GetButton(InputAxes.HornInput);
    }

    public bool GetEngineInput()
    {
        return Input.GetButtonDown(InputAxes.EngineInput);
    }

    public bool GetResetInput()
    {
        return Input.GetButtonDown(InputAxes.ResetInput);
    }
}
