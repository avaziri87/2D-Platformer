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

        if(OnPikcUp != null) OnPikcUp.Invoke();

        gameObject.SetActive(false);
    }
}