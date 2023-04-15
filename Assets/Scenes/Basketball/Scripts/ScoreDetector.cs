using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    private const string basketballTag = "basketball";

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(basketballTag))
        {
            ++score;
            Debug.LogError(score);
        }
    }
}
