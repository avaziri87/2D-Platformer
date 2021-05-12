using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 5;
    [SerializeField] Sprite _downSprite = null;
    
    AudioSource _audioSource;
    SpriteRenderer _spriteRender;
    Sprite _upSprite = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRender.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();

        if (player == null) return;

        var rigidbody2D = player.GetComponent<Rigidbody2D>();

        if (rigidbody2D == null) return;

        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);

        _spriteRender.sprite = _downSprite;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();

        if (player == null) return;

        _spriteRender.sprite = _upSprite;

        if (_audioSource != null) _audioSource.Play();
    }
}
