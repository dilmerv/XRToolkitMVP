using DilmerGames.Core.Singletons;
using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Manager Options")]
    [SerializeField]
    private XROrigin xrOrigin;

    [field: SerializeField]
    public GameState GameState { get; private set; } = GameState.Playing;

    public XROrigin Player { get { return xrOrigin; } }

    [Header("Events")]
    public Action<GameState> OnGamePaused;

    public Action<GameState> OnGameResumed;

    private LayerMask cachedCameraCullingMask;

    private void Awake()
    {
        cachedCameraCullingMask = xrOrigin.Camera.cullingMask;
        ControllerManager.Instance.OnControllerMenuActionExecuted += ChangeGameState;
        UIManager.Instance.OnGameResumeActionExecuted += ChangeGameState;
    }

    private void OnDestroy()
    {
        ControllerManager.Instance.OnControllerMenuActionExecuted -= ChangeGameState;
        UIManager.Instance.OnGameResumeActionExecuted -= ChangeGameState;
    }

    private void ChangeGameState()
    {
        GameState = GameState == GameState.Playing ? GameState.Paused : GameState.Playing;

        if (GameState == GameState.Paused)
        {
            OnGamePaused?.Invoke(GameState.Paused);
            Time.timeScale = 0;
            xrOrigin.Camera.cullingMask = LayerMask.GetMask("UI");
        }
        else
        {
            OnGameResumed?.Invoke(GameState.Playing);
            Time.timeScale = 1;
            xrOrigin.Camera.cullingMask = cachedCameraCullingMask;
        }
    }
}