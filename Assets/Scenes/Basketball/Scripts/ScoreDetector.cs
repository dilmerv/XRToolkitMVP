using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText = null;

    private const string basketballTag = "basketball";

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(basketballTag))
        {
            ++score;
            if(scoreText != null)
            {
                scoreText.text = score.ToString();
            }
            Debug.LogError(score);
        }
    }
}
