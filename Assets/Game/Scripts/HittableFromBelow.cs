using System;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _emptyBoxSprite;

    Animator _animator;
    AudioSource _audioSource;

    protected virtual bool CanUse => true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CanUse) return;

        var player = collision.collider.GetComponent<Player>();
        if (player == null) return;

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            PlayAudio();
            Use();
            if (!CanUse) GetComponent<SpriteRenderer>().sprite = _emptyBoxSprite;
        }
    }

    private void PlayAudio()
    {
        if (_audioSource != null) _audioSource.Play();
    }

    protected abstract void Use();

    private void PlayAnimation()
    {
        if (_animator != null) _animator.SetTrigger("Hit");
    }
}
