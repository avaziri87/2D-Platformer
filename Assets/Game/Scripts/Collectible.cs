using System;
using System.Collections.Generic;
using UnityEngine;
public class Collectible: MonoBehaviour
{
    public event Action OnPikcUp;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if(OnPikcUp != null) OnPikcUp.Invoke();

        var audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}