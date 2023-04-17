using DilmerGames.Core.Singletons;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music Tracks")]
    [SerializeField]
    private AudioClip[] tracks;

    private AudioSource audioSource;

    [Header("Events")]
    public Action onCurrentTrackEnded;

    private void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SuffleWhenItStopsPlaying());
        ShuffleAndPlay();
    }

    private void ShuffleAndPlay(GameState gameState = GameState.Playing)
    {
        if (tracks.Length > 0)
        {
            audioSource.clip = tracks[UnityEngine.Random.Range(0, tracks.Length - 1)];
            audioSource.Play();
        }
    }

    private IEnumerator SuffleWhenItStopsPlaying()
    { 
        while(true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            onCurrentTrackEnded?.Invoke();
            ShuffleAndPlay();
        }
    }
}
