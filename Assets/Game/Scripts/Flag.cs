using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) return;

        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        string key = _sceneName + "Unlock";
        PlayerPrefs.SetInt(key, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneName);
    }
}
