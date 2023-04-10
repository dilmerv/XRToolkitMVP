using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeature : MonoBehaviour
{
    [Header("Door Configuration")]
    [SerializeField]
    private Transform doorPivot;

    [SerializeField]
    private float minAngle = 0;

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private bool open = false;

    [Header("Socket Configuration")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    private void Start()
    {
        StartCoroutine(ProcessDoorMotion());

        socketInteractor?.selectEntered.AddListener((s) =>
        {
            open = true;
        });
    }

    private IEnumerator ProcessDoorMotion()
    { 
        while(true)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y :
                doorPivot.localEulerAngles.y - 360;

            if (open && angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, speed * Time.deltaTime);
            }
            else if(!open && angle > minAngle)
            {
                doorPivot?.Rotate(Vector3.up, -speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
