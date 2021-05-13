using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [Header("Movement Parameters")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _slipFactor = 1;
    [SerializeField] float _acceleration = 1;
    [SerializeField] float _breaking = 1;
    [SerializeField] float _airAcceleration = 1;
    [SerializeField] float _airBreaking = 1;
    [Header("Jump Parameters")]
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] float _wallSlideSpeed = 1;

    Rigidbody2D _rigidbody2D;
    Animator    _animator;
    SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    Vector3 _startPos;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    float _horizontal;
    bool _isGrounded;
    bool _isOnSlipperyGround;
    string _jumpButton;
    string _horizontalAxis;
    int _layerMask;

    public int PlayerNumber => _playerNumber;

    void Start()
    {
        _startPos = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        _layerMask = LayerMask.GetMask("Default");
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

        if (ShouldSlide())
        {
            if (CanWallJump())
                WallJump();
            else
                Slide();
            return;
        }
        if (CanJump()) 
            Jump();
        else if (CanDoubleJump()) 
            DoubleJump();

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
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
    }
    void MoveHorizontal()
    {
        float smoothnessMultiplier = _horizontal == 0 ? _breaking : _acceleration;
        if(!_isGrounded) smoothnessMultiplier = _horizontal == 0 ? _airBreaking : _airAcceleration;

        float newHorizontal = Mathf.Lerp(_rigidbody2D.velocity.x, _horizontal * _speed, Time.deltaTime * smoothnessMultiplier);
        _rigidbody2D.velocity = new Vector2(newHorizontal, _rigidbody2D.velocity.y);
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
        _animator.SetBool("Slide", ShouldSlide());
    }
    void CheckIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);
        _isGrounded = hit != null;

        if (hit != null)
            _isOnSlipperyGround = hit.CompareTag("Slippery");
        else
            _isOnSlipperyGround = false;
    }
    bool CanJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }
    void Jump()
    {
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        if(_audioSource != null) _audioSource.Play();
    }
    bool CanDoubleJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }
    void DoubleJump()
    {
        _fallTimer = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }
    bool ShouldSlide()
    {
        if (_isGrounded) return false;

        if (_rigidbody2D.velocity.y > 0) return false;

        if(_horizontal < 0)
        {
            var hit = Physics2D.OverlapCircle(_leftSensor.position, 0.1f);
            if(hit != null && hit.CompareTag("Wall")) return true;
        }
        if (_horizontal > 0)
        {
            var hit = Physics2D.OverlapCircle(_rightSensor.position, 0.1f);
            if (hit != null && hit.CompareTag("Wall")) return true;
        }
            return false;
    }
    void Slide()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, - _wallSlideSpeed);
    }
    private bool CanWallJump()
    {
        return Input.GetButtonDown(_jumpButton);
    }
    private void WallJump()
    {
        Debug.Log("wall jump");
        _rigidbody2D.velocity = new Vector2(-_horizontal * _jumpVelocity, _jumpVelocity * 1.5f);
    }
    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPos;
        //SceneManager.LoadScene("Menu Scene");
    }    
    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}