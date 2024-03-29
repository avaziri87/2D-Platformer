using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite = null;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;
    [SerializeField] int _playerNumber = 1;
    [SerializeField] AudioClip _onPressedCip;
    [SerializeField] AudioClip _onReleaseCip;

    Sprite _releasedSprite;
    SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _releasedSprite = _spriteRenderer.sprite;
        BecomeRelease();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null || player.PlayerNumber != _playerNumber) return;

        BecomePressed();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null || player.PlayerNumber != _playerNumber) return;

        BecomeRelease();
    }

    void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite;
        if (_audioSource != null) _audioSource.PlayOneShot(_onPressedCip);

        _onPressed?.Invoke();
    }

    void BecomeRelease()
    {
        if (_onReleased.GetPersistentEventCount() == 0) return;

        _spriteRenderer.sprite = _releasedSprite;
        if (_audioSource != null) _audioSource.PlayOneShot(_onReleaseCip);

        _onReleased?.Invoke();
    }

}
