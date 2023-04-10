using UnityEngine;

public class FlashlightFeature : BaseFeature
{
    [SerializeField]
    private bool on = false;

    public void ToggleFlashLight()
    {
        on = !on;
        GetComponentInChildren<Light>().enabled = on;
        if (on) PlayOnStarted();
        else PlayOnEnded();
    }
}
