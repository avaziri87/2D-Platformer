using TMPro;
using UnityEngine;

public class UIHighScore : MonoBehaviour
{
    void Start()
    {
        int value = PlayerPrefs.GetInt("HighScore");
        GetComponent<TMP_Text>().SetText(value.ToString());
    }

    [ContextMenu("Clear High Score")]
    void ClearHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
