using DilmerGames.Core.Singletons;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Vector3 initialPositionFromPlayer = Vector3.forward;

    private const string MENU_SCENE_NAME = "Menu";

    private void OnEnable()
    {
        var playerHead = GameManager.Instance.Player.Camera.transform;
        transform.position = playerHead.position + initialPositionFromPlayer;
        transform.rotation = playerHead.rotation;
    }

    public void HandleGameState()
    {
        if (GameManager.Instance.GameState == GameState.Paused)
        {
            StartCoroutine(UnloadMenu(() =>
            {
                GameManager.Instance.GameState = GameState.Playing;
                Time.timeScale = 1;
            }));
        }
        else
        {
            StartCoroutine(AddMenu(() =>
            {
                GameManager.Instance.GameState = GameState.Paused;
                Time.timeScale = 0;
            }));
        }
    }

    private IEnumerator UnloadMenu(Action callBack)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(MENU_SCENE_NAME);
        yield return unloadOperation;
        callBack?.Invoke();
    }

    private IEnumerator AddMenu(Action callBack)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(MENU_SCENE_NAME, LoadSceneMode.Additive);
        yield return loadOperation;
        callBack?.Invoke();
    }

    public void PauseGame()
    { 
        GameManager.Instance.GameState = GameState.Paused;
    }

    public void ResumeGame()
    {
        GameManager.Instance.GameState = GameState.Playing;
    }
}
