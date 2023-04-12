using DilmerGames.Core.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerManager : Singleton<ControllerManager>
{
    [SerializeField]
    private InputActionProperty controllerMenuAction;

    private XRRayInteractor[] cachedRayInteractors;

    private void Awake()
    {
        cachedRayInteractors = FindObjectsByType<XRRayInteractor>(FindObjectsInactive.Include,
               FindObjectsSortMode.InstanceID);
        ControllerRayInteractorsInput(active: false);

        controllerMenuAction.action.performed += ControllerMenuActionPerformed;
    }

    private void ControllerMenuActionPerformed(InputAction.CallbackContext obj)
    {
        UIManager.Instance.HandleGameState();
    }

    public void ControllerRayInteractorsInput(bool active)
    { 
        foreach(var rayInteractor in cachedRayInteractors)
        {
            rayInteractor.gameObject.SetActive(active);
        }
    }
}
