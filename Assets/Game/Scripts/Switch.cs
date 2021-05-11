using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;
    [Header("sprite used in each state")]
    [SerializeField] Sprite _rightSprite;
    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _centerSprite;
    [Header("Events for toggle switch")]
    [SerializeField] UnityEvent _rightActive;
    [SerializeField] UnityEvent _leftActive;
    [SerializeField] UnityEvent _centerActive;

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    enum ToggleDirection
    {
        Left,
        Center,
        Right
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        var playerRigibody = player.GetComponent<Rigidbody2D>();
        if (playerRigibody == null) return;

        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigibody.velocity.x > 0;
        bool playerWalkingLeft = playerRigibody.velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
            SetToggleDirection(ToggleDirection.Right);
        else if (!wasOnRight && playerWalkingLeft)
            SetToggleDirection(ToggleDirection.Left);
    }

    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        if (!force && _currentDirection == direction) return;

        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _leftSprite;
                _leftActive?.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _centerSprite;
                _centerActive?.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _rightSprite;
                _rightActive?.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        switch (_startingDirection)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _leftSprite;
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _centerSprite;
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _rightSprite;
                break;
            default:
                break;
        }
    }
}
