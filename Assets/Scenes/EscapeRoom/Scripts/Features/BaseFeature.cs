using UnityEngine;

public class BaseFeature : MonoBehaviour
{
    public bool SFXAudioSourceCreated { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipForOnStarted { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipForOnEnded { get; set; }

    private AudioSource audioSource;

    [SerializeField]
    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    { 
        CreateSFXAudioSource();
    }

    private void CreateSFXAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        SFXAudioSourceCreated = true;
    }

    protected void PlayOnStarted()
    {
        if (SFXAudioSourceCreated && AudioClipForOnStarted != null)
        { 
            audioSource.clip = AudioClipForOnStarted;
            audioSource.Play();
        }
    }

    protected void PlayOnEnded()
    {
        if (SFXAudioSourceCreated && AudioClipForOnEnded != null)
        {
            audioSource.clip = AudioClipForOnEnded;
            audioSource.Play();
        }
    }
}
