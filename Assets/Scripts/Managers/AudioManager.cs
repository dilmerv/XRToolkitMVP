using DilmerGames.Core.Singletons;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music Tracks")]
    [SerializeField]
    private AudioClip[] tracks;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay()
    {
        if (tracks.Length > 0)
        {
            audioSource.clip = tracks[Random.Range(0, tracks.Length - 1)];
            audioSource.Play();
        }
    }
}
