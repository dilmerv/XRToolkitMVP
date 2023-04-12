using DilmerGames.Core.Singletons;
using System;
using System.Collections;
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
    public Action OnGameResumeActionExecuted;

    public void Awake()
    {
        // bind to game manager events
        GameManager.Instance.OnGamePaused += HandleMenuScene;
        GameManager.Instance.OnGameResumed += HandleMenuScene;

        // bind menu buttons
        var menu = menuContainer.GetComponentInChildren<Menu>(true);

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuScene(GameState.Playing);
            OnGameResumeActionExecuted?.Invoke();
        });

        menu.RestartButton.onClick.AddListener(() => StartCoroutine(RestartMainScene()));
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= HandleMenuScene;
        GameManager.Instance.OnGameResumed -= HandleMenuScene;
    }

    private void HandleMenuScene(GameState gameState)
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
        var playerHead = GameManager.Instance.Player.Camera.transform;
        menuContainer.transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        menuContainer.transform.rotation = playerHead.rotation;
    }

    private IEnumerator RestartMainScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        yield return loadOperation;
    }
}
