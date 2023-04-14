using DilmerGames.Core.Singletons;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Options")]
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f;

    [SerializeField]
    private GameObject menuContainer;

    private const string GAME_SCENE_NAME = "Game";

    [Header("Events")]
    public Action OnGameResumedActionExecuted;

    public void Awake()
    {
        // bind to game manager events
        GameManager.Instance.OnGamePaused += HandleMenuOptions;
        GameManager.Instance.OnGameResumed += HandleMenuOptions;

        // bind menu buttons
        var menu = menuContainer.GetComponentInChildren<Menu>(true);

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuOptions(GameState.Playing);
            OnGameResumedActionExecuted?.Invoke();
        });

        menu.RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
            OnGameResumedActionExecuted?.Invoke();
        });
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= HandleMenuOptions;
        GameManager.Instance.OnGameResumed -= HandleMenuOptions;
    }

    private void HandleMenuOptions(GameState gameState)
    {
        if (gameState == GameState.Paused)
        {
            menuContainer.SetActive(true);
            PlaceMenuInFrontOfPlayer();
        }
        else
        {
            menuContainer.SetActive(false);
        }
    }

    private void PlaceMenuInFrontOfPlayer()
    {
        // place UI in front of the player
        var playerHead = Camera.main.transform;
        menuContainer.transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        menuContainer.transform.rotation = playerHead.rotation;
    }
}
