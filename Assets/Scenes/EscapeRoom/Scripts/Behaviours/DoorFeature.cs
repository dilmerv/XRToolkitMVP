using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeature : BaseFeature
{
    [Header("Door Configuration")]
    [SerializeField]
    private Transform doorPivot;

    [SerializeField]
    private float minAngle = 0;

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private bool open = false;

    public bool Open 
    {
        get => open;
        set => open = value;
    }

    [Header("Interaction Configuration")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        StartCoroutine(ProcessDoorMotion());

        // doors with sockets
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            open = true;
            PlayOnStarted();
        });

        socketInteractor?.selectExited.AddListener((s) =>
        {
            PlayOnEnded();
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true;
        });

        // doors with simple selections (no sockets)
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            if (!open)
            {
                open = true;
                PlayOnStarted();
            }
        });
    }

    private IEnumerator ProcessDoorMotion()
    { 
        while(true)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y :
                doorPivot.localEulerAngles.y - 360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if (open && angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, speed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }
            else if(!open && angle > minAngle)
            {
                doorPivot?.Rotate(Vector3.up, -speed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }
            yield return null;
        }
    }
}
