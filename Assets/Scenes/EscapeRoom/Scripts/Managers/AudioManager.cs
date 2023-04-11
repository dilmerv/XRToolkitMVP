using DilmerGames.Core.Singletons;
using System.Collections;
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
        StartCoroutine(ShuffleWhenItStopsPlaying());
    }

    public void ShuffleAndPlay()
    {
        if (tracks.Length > 0)
        {
            audioSource.clip = tracks[Random.Range(0, tracks.Length - 1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenItStopsPlaying()
    {
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
        }
    }
}
