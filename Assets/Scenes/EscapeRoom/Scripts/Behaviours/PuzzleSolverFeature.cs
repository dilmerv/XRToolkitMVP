using DilmerGames.Core.Singletons;
using System;
using UnityEngine;

public class PuzzleSolverFeature : Singleton<PuzzleSolverFeature>
{
    [SerializeField]
    private string playerTag;

    public Action<GameState> onPuzzleSolved;

    private bool puzzleSolvedDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag && !puzzleSolvedDetected)
        {
            onPuzzleSolved?.Invoke(GameState.PuzzleSolved);
            puzzleSolvedDetected = true;
        }
    }
}
