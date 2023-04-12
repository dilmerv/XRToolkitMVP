using DilmerGames.Core.Singletons;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private XROrigin xrOrigin;

    [SerializeField]
    private UnityEvent OnGameStarted;

    [SerializeField]
    private UnityEvent OnGameLost;

    [field: SerializeField]
    public GameState GameState { get; set; } = GameState.Playing;

    public XROrigin Player { get { return xrOrigin; } }

    void Start() => AudioManager.Instance.ShuffleAndPlay();
}