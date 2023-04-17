using DilmerGames.Core.Singletons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerManager : Singleton<ControllerManager>
{
    [Header("Controller Mapping")]
    [SerializeField]
    private InputActionProperty controllerMenuAction;

    private XRRayInteractor[] cachedRayInteractors;

    [Header("Events")]
    public Action onControllerMenuActionExecuted;

    private void Awake()
    {
        cachedRayInteractors = FindObjectsByType<XRRayInteractor>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
    }

    private void OnEnable()
    {
        // bind controller events
        controllerMenuAction.action.performed += ControllerMenuActionPerformed;

        // bind to game manager envets
        GameManager.Instance.onGamePaused += ControllerRayInteractorInput;
        GameManager.Instance.onGameResumed += ControllerRayInteractorInput;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGamePaused -= ControllerRayInteractorInput;
        GameManager.Instance.onGameResumed -= ControllerRayInteractorInput;
    }

    private void ControllerMenuActionPerformed(InputAction.CallbackContext obj)
    {
        onControllerMenuActionExecuted?.Invoke();
    }

    private void ControllerRayInteractorInput(GameState gameState = GameState.Playing)
    {
        foreach (var rayInteractor in cachedRayInteractors)
        {
            rayInteractor.gameObject.SetActive(gameState == GameState.Paused);
            if (gameState == GameState.Paused)
            {
                ApplyDefaultLayers(rayInteractor.transform.parent,"UI");
            }
            else
            {
                ApplyDefaultLayers(rayInteractor.transform.parent,"Default");
            }
        }
    }

    private void ApplyDefaultLayers(Transform rayParent, string layerName)
    {
        LayerMask uiLayerMask = LayerMask.NameToLayer(layerName);
        rayParent.gameObject.layer = uiLayerMask;
        foreach (Transform transform in rayParent.GetComponentsInChildren<Transform>())
        { 
            transform.gameObject.layer = uiLayerMask;
        }
    }
}
