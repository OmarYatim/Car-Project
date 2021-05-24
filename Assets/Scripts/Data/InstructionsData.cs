using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInstructionsContainer", menuName ="InstructionsContainer")]
public class InstructionsData : ScriptableObject
{
    public string EngineInstructions;
    public string DrivingInstructions;
    public string BrakeInstructions;
    public string HornInstructions;
    public string ResetInstructions;
}
