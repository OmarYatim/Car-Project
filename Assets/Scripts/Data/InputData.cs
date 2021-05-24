using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInputContainer", menuName = "InputContainer")]
public class InputData : ScriptableObject
{
    public string HorizontalInput;
    public string VerticalInput;
    public string BrakeInput;
    public string EngineInput;
    public string HornInput;
    public string ResetInput;
}
