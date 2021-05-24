using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Instance;
    
    [SerializeField] float maxpitch = 4;
    [SerializeField] float minpitch = 0.2f;
    [SerializeField] SoundData SoundClips;


    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator PlayStartSound(AudioSource source)
    {
        source.clip = SoundClips.StartSound;
        source.pitch = 1;
        source.Play();
        Debug.Log("bde");
        yield return new WaitForSeconds(SoundClips.StartSound.length);
        GameManager.Instance.state = GameState.EngineStarted;
    }

    public void PlayEngineSound(AudioSource source, double speed, float maxspeed)
    {
        if(source.clip != SoundClips.EngineSound)
        {
            source.clip = SoundClips.EngineSound;
            source.loop = true;
            source.pitch = 1;
            source.Play();
        }
        source.pitch = (float)(speed / maxspeed) * maxpitch;
        if (source.pitch < minpitch)
            source.pitch = minpitch;
    }
}
