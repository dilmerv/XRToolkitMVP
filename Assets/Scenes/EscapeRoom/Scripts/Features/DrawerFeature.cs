using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeature : BaseFeature
{
    [Header("Drawer Configuration")]
    [SerializeField]
    private Transform drawerPivot;

    [SerializeField]
    private float maxDistance = 0.25f;

    [SerializeField]
    private FeatureDirection featureDirection = FeatureDirection.Forward;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private bool open = false;

    [Header("Interaction Configuration")]
    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        // drawers with simple interactable selection
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            if(!open)
            {
                OpenDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        open = true;
        PlayOnStarted();
        StartCoroutine(ProcessMotion());
    }


    private IEnumerator ProcessMotion()
    { 
        while(open)
        {
            if (featureDirection == FeatureDirection.Forward && drawerPivot.localPosition.z <= maxDistance)
            {
                drawerPivot.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (featureDirection == FeatureDirection.Backward && drawerPivot.localPosition.z >= maxDistance)
            {
                drawerPivot.Translate(-Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                open = false;
            }
            yield return null;
        }
    }

}
