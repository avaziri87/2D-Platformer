using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float     _speed = 5.0f;
    [SerializeField] float     _jumpVelocity = 10;
    [SerializeField] float     _downPull = 5;
    [SerializeField] int       _maxJumps = 2;
    [SerializeField] Transform _feet;

    Vector3 _startPos;
    int     _jumpsRemaining;
    float   _fallTimer;

    void Start()
    {
        _startPos = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));
        bool isGrounded = hit != null;

        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();

        if(Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
        }

        var animator = GetComponent<Animator>();
        bool isWalking = horizontal != 0;
        animator.SetBool("Walking", isWalking);

        if(horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if(Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        {
            _jumpsRemaining--;
            _fallTimer = 0;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpVelocity);
        }

        if(isGrounded)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y - downForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPos;
    }
}