using UnityEngine;

public class UILockable : MonoBehaviour
{
    void OnEnable()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.SceneName + "Unlock";
        int unlocked = PlayerPrefs.GetInt(key, 0);

        if (unlocked == 0) gameObject.SetActive(false);
    }

    [ContextMenu("Lock Level")]
    void ClearLevelUnlock()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.SceneName + "Unlock";
        PlayerPrefs.DeleteKey(key);
    }

    [ContextMenu("Unlock Level")]
    void UnlcokLevel()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.SceneName + "Unlock";
        PlayerPrefs.GetInt(key, 1);
    }
}
