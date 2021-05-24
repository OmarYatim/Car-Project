using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    StartGame,
    EngineStarted,
    CarReset,
    EngineStopped
}
public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public GameState state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = GameState.StartGame;
    }
}
