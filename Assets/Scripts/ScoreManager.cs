using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Delegate and Event
    public delegate void OnScoreChanged(int newScore);
    public static event OnScoreChanged ScoreChanged;

    private int _score;

    public void AddScore(int amount)
    {
        _score += amount;
        ScoreChanged?.Invoke(_score); //trigger
    }

    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        //subscribe
        ScoreManager.ScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        //unsubscribe
        ScoreManager.ScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
