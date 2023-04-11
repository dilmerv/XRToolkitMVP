using DilmerGames.Core.Singletons;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private UnityEvent OnGameStarted;

    [SerializeField]
    private UnityEvent OnGameLost;

    void Start()
    {
        AudioManager.Instance.ShuffleAndPlay();
    }
}
