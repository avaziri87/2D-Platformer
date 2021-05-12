using System;
using UnityEngine;

public static class ScoreSystem
{
    public static event Action<int> OnScoreChange;

    static int _highScore;

    public static int Score { get; private set; }

    public static void Add(int points)
    {
        if(_highScore == 0) _highScore = PlayerPrefs.GetInt("HighScore");

        Score += points;
        OnScoreChange?.Invoke(Score);

        if(Score > _highScore)
        {
            _highScore = Score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        Debug.Log($"High Score = {_highScore}");
    }
}
