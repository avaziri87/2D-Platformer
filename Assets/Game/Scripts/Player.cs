using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
    }
}
