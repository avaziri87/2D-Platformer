using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;
    [SerializeField] float _bounceForce = 5;
    [SerializeField] int _bouncesRemaining = 3;

    Rigidbody2D _rigibody;

    public int Direction { get; set; }

    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _rigibody.velocity = Vector2.right * _launchForce * Direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage takeDamage = collision.collider.GetComponent<ITakeDamage>();

        if(takeDamage != null)
        {
            takeDamage.TakeDamage();
            Destroy(gameObject);
            return;
        }

        _bouncesRemaining--;
        if (_bouncesRemaining < 0)
            Destroy(gameObject);
        else
            _rigibody.velocity = new Vector2(_launchForce * Direction, _bounceForce);
    }
}
