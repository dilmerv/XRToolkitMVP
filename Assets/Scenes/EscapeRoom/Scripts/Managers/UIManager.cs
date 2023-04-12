using DilmerGames.Core.Singletons;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f;

    private const string GAME_SCENE_NAME = "Game";

    private const string MENU_SCENE_NAME = "Menu";

    private void OnEnable()
    {
        var playerHead = GameManager.Instance.Player.Camera.transform;
        transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        transform.rotation = playerHead.rotation;
    }

    public void HandleGameState()
    {
        GameManager.Instance.GameState = GameManager.Instance.GameState == GameState.Playing ?
            GameState.Paused : GameState.Playing;

        if (GameManager.Instance.GameState == GameState.Paused)
        {
            Time.timeScale = 0;
            ControllerManager.Instance.ControllerRayInteractorsInput(active: true);
            StartCoroutine(AddMenuScene());
        }
        else
        {
            Time.timeScale = 1;
            ControllerManager.Instance.ControllerRayInteractorsInput(active: false);
            StartCoroutine(UnloadMenuScene());
        }
    }

    private IEnumerator UnloadMenuScene()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(MENU_SCENE_NAME);
        yield return unloadOperation;
    }

    private IEnumerator AddMenuScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(MENU_SCENE_NAME, LoadSceneMode.Additive);
        yield return loadOperation;
    }
    private IEnumerator RestartMainScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        yield return loadOperation;
    }

    public void PauseGame()
    { 
        GameManager.Instance.GameState = GameState.Paused;
    }

    public void ResumeGame()
    {
        GameManager.Instance.GameState = GameState.Playing;
    }

    public void RestartGame()
    {
        StartCoroutine(RestartMainScene());
    }
}
