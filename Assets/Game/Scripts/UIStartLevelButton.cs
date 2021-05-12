using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string _sceneName;

    public string SceneName => _sceneName;


    public void LoadLevel()
    {
        string key = _sceneName + "Unlock";
        PlayerPrefs.SetInt(key, 1);
        SceneManager.LoadScene(_sceneName);
    }
}