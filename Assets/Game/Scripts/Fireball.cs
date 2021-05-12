using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * _launchForce;
    }
}
