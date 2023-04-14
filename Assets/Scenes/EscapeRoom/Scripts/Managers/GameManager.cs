using DilmerGames.Core.Singletons;
using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    public GameState GameState { get; private set; } = GameState.Playing;

    [Header("Events")]
    public Action<GameState> OnGamePaused;

    public Action<GameState> OnGameResumed;

    private LayerMask cachedCameraCullingMask;

    private void Awake()
    {
        cachedCameraCullingMask = Camera.main.cullingMask;
        ControllerManager.Instance.OnControllerMenuActionExecuted += ChangeGameState;
        UIManager.Instance.OnGameResumedActionExecuted += ChangeGameState;
    }

    private void OnDestroy()
    {
        ControllerManager.Instance.OnControllerMenuActionExecuted -= ChangeGameState;
        UIManager.Instance.OnGameResumedActionExecuted -= ChangeGameState;
    }

    private void ChangeGameState()
    {
        GameState = GameState == GameState.Playing ? GameState.Paused : GameState.Playing;

        if (GameState == GameState.Paused)
        {
            OnGamePaused?.Invoke(GameState.Paused);
            Time.timeScale = 0;
            Camera.main.cullingMask = LayerMask.GetMask("UI");
        }
        else
        {
            OnGameResumed?.Invoke(GameState.Playing);
            Time.timeScale = 1;
            Camera.main.cullingMask = cachedCameraCullingMask;
        }
    }
}