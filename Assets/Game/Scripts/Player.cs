using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [Header("Movement Parameters")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _slipFactor = 1;
    [Header("Jump Parameters")]
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;

    Rigidbody2D _rigidbody2D;
    Animator    _animator;
    SpriteRenderer _spriteRenderer;

    Vector3 _startPos;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    float _horizontal;
    bool _isGrounded;
    bool _isOnSlipperyGround;

    public int PlayerNumber => _playerNumber;

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
        
        if(_isOnSlipperyGround)
            MoveSlipperyHorizontal();
        else
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
        _horizontal = Input.GetAxis($"P{_playerNumber}Horizontal") * _speed;
    }
    void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }
    void MoveSlipperyHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(
            _rigidbody2D.velocity,
            desiredVelocity,
            Time.deltaTime / _slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;
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
        _animator.SetBool("Jump", CanDoubleJump());
    }
    void CheckIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));
        _isGrounded = hit != null;

        if (hit != null)
            _isOnSlipperyGround = hit.CompareTag("Slippery");
        else
            _isOnSlipperyGround = false;
    }
    bool CanJump()
    {
        return Input.GetButtonDown($"P{_playerNumber}Jump") && _jumpsRemaining > 0;
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
        return Input.GetButton($"P{_playerNumber}Jump") && _jumpTimer <= _maxJumpDuration;
    }
    void DoubleJump()
    {
        _fallTimer = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }
    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPos;
    }    
    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}