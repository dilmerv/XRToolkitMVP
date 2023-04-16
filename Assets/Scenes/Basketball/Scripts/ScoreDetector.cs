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
            Vector3 dir = other.transform.position - transform.position;

            // Only add score when the ball is entered from the top
            if(dir.y > 0)
            {
                ++score;
                if (scoreText != null)
                {
                    scoreText.text = score.ToString();
                }
            }
        }
    }
}
