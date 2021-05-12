using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChange += UpdateScoreText;
        UpdateScoreText(ScoreSystem.Score);
    }

    private void UpdateScoreText(object score)
    {
        throw new NotImplementedException();
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChange -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        _text.SetText(score.ToString());
    }
}
