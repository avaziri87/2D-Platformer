using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlock;

    public void Unlock()
    {
        Debug.Log("Unlocked!");
        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null) audioSource.Play();
        _onUnlock.Invoke();
    }
}
