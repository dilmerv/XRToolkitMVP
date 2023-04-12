using DilmerGames.Core.Singletons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerManager : Singleton<ControllerManager>
{
    [Header("Controller Mappings")]
    [SerializeField]
    private InputActionProperty controllerMenuAction;

    private XRRayInteractor[] cachedRayInteractors;

    [Header("Events")]
    public Action OnControllerMenuActionExecuted;

    private void Awake()
    {
        cachedRayInteractors = FindObjectsByType<XRRayInteractor>(FindObjectsInactive.Include,
               FindObjectsSortMode.InstanceID);

        ControllerRayInteractorsInput();

        // bind to controller events
        controllerMenuAction.action.performed += ControllerMenuActionPerformed;

        // bind to game manager events
        GameManager.Instance.OnGamePaused += ControllerRayInteractorsInput;
        GameManager.Instance.OnGameResumed += ControllerRayInteractorsInput;
    }


    private void OnDestroy()
    {
        controllerMenuAction.action.performed -= ControllerMenuActionPerformed;
        GameManager.Instance.OnGamePaused -= ControllerRayInteractorsInput;
        GameManager.Instance.OnGameResumed -= ControllerRayInteractorsInput;
    }

    private void ControllerMenuActionPerformed(InputAction.CallbackContext obj)
    {
        OnControllerMenuActionExecuted?.Invoke();
    }

    public void ControllerRayInteractorsInput(GameState gameState = GameState.Playing)
    { 
        foreach(var rayInteractor in cachedRayInteractors)
        {
            rayInteractor.gameObject.SetActive(gameState == GameState.Paused);
            if(gameState == GameState.Paused)
            {
                ApplyDefaultLayers(rayInteractor.transform.parent, "UI");
            }
            else
            {
                ApplyDefaultLayers(rayInteractor.transform.parent, "Default");
            }
        }
    }

    private static void ApplyDefaultLayers(Transform rayParent, string layerName)
    {
        LayerMask uiLayerMask = LayerMask.NameToLayer(layerName);
        rayParent.gameObject.layer = uiLayerMask;
        foreach (Transform transform in rayParent.GetComponentsInChildren<Transform>())
        {
            transform.gameObject.layer = uiLayerMask;
        }
    }
}
