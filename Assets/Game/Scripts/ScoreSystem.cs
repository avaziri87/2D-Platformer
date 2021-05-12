using System;
using UnityEngine;

public static class ScoreSystem
{
    public static event Action<int> OnScoreChange;

    static int _score;
    static int _highScore;

    public static void Add(int points)
    {
        if(_highScore == 0) _highScore = PlayerPrefs.GetInt("HighScore");

        _score += points;
        OnScoreChange?.Invoke(_score);

        if(_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        Debug.Log($"High Score = {_highScore}");
    }
}
