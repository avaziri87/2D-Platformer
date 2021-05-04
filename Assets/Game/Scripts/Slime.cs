using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftGroundCheck = null;
    [SerializeField] Transform _rightGroundCheck = null;

    Rigidbody2D _rigidbody2D = null;

    float _direction = -1;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y);

        if (_direction < 0) 
            ScanSensor(_leftGroundCheck);
        else 
            ScanSensor(_rightGroundCheck);
    }

    private void ScanSensor(Transform sensor)
    {
        var downtHit = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (downtHit.collider == null) TurnAround();

        var sideHit = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideHit.collider != null) TurnAround();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();

        if (player == null) return;

        player.ResetToStart();
    }

    private void TurnAround()
    {
        _direction *= -1;
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0;
    }

}
