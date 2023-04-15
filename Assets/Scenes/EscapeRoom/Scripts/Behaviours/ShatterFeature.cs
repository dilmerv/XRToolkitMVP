using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterFeature : BaseFeature
{
    [SerializeField]
    private GameObject fracturedObject = null;

    private GameObject fractureObj = null;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("Entering collision");
        Fracture();
    }

    public void Fracture()
    {
        if(fracturedObject == null)
        {
            return;
        }

        this.gameObject.SetActive(false);

        fractureObj = Instantiate(fracturedObject, this.transform.position, this.transform.rotation);
    }
}
