using System;
using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftGroundCheck = null;
    [SerializeField] Transform _rightGroundCheck = null;
    [SerializeField] Sprite _deadSprite = null;

    Rigidbody2D _rigidbody2D = null;

    float _direction = -1;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

        var contactPoint2D = collision.contacts[0];
        Vector2 normal = contactPoint2D.normal;

        if(normal.y <= -0.5f)
        {
            StartCoroutine(Die());
        }
        else
        {
            player.ResetToStart();
        }
    }

    IEnumerator Die()
    {
        _spriteRenderer.sprite = _deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null) audioSource.Play();

        float alpha = 1;
        while (alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
        }

        enabled = false;
    }

    private void TurnAround()
    {
        _direction *= -1;
        _spriteRenderer.flipX = _direction > 0;
    }
}
