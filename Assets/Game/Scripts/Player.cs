using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _jumpForce = 200;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;

    Vector3 _startPos;
    int _jumpsRemaining;

    void Start()
    {
        _startPos = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    void Update()
    {
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
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));

        if(hit != null)
        {
            _jumpsRemaining = _maxJumps;
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPos;
    }
}