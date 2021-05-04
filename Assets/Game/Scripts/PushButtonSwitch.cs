using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite = null;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;

    Sprite _releasedSprite;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        BecomeRelease();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) return;

        BecomePressed();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) return;

        BecomeRelease();
    }

    void BecomeRelease()
    {
        _spriteRenderer.sprite = _releasedSprite;

        _onReleased?.Invoke();
    }

    void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite;

        _onPressed?.Invoke();
    }
}
