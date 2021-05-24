using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewAudioContainer",menuName ="AudioContainer")]
public class SoundData : ScriptableObject
{
    public AudioClip StartSound;
    public AudioClip EngineSound;
    public AudioClip BrakeSound;
    public AudioClip HornSound;
}
