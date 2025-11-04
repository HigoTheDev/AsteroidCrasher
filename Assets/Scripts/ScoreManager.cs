using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public System.Action<int> OnScoreChanged;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ResetScore();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        OnScoreChanged?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
        OnScoreChanged?.Invoke(score);
    }

    private void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = score.ToString("00");
        }
    }

    public int GetScore() => score;
}
