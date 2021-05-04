using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float     _speed = 5.0f;
    [SerializeField] float     _jumpVelocity = 10;
    [SerializeField] float     _downPull = 5;
    [SerializeField] float     _maxJumpDuration = 0.1f;
    [SerializeField] int       _maxJumps = 2;
    [SerializeField] Transform _feet;

    Rigidbody2D _rigidbody2D;
    Animator    _animator;
    SpriteRenderer _spriteRenderer;

    Vector3 _startPos;
    int     _jumpsRemaining;
    float   _fallTimer;
    float   _jumpTimer;
    float   _horizontal;
    bool    _isGrounded;

    void Start()
    {
        _startPos = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckIsGrounded();
        GetInput();
        MoveHorizontal();
        UpdateAnimator();
        UpdateSprite();

        if (CanJump()) Jump();
        else if (CanDoubleJump()) DoubleJump();


        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }
    }

    void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed;
    }
    void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1)
        {
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        }
    }
    void UpdateSprite()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }
    void UpdateAnimator()
    {
        bool isWalking = _horizontal != 0;
        _animator.SetBool("Walking", isWalking);
    }
    void CheckIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));
        _isGrounded = hit != null;
    }
    bool CanJump()
    {
        return Input.GetButtonDown("Fire1") && _jumpsRemaining > 0;
    }
    void Jump()
    {
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }
    bool CanDoubleJump()
    {
        return Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration;
    }
    void DoubleJump()
    {
        _fallTimer = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }

    internal void ResetToStart()
    {
        transform.position = _startPos;
    }
}