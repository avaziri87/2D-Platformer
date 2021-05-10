using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _emptyBoxSprite;

    Animator _animator;
    protected virtual bool CanUse => true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CanUse) return;

        var player = collision.collider.GetComponent<Player>();
        if (player == null) return;

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            Use();
            if (!CanUse) GetComponent<SpriteRenderer>().sprite = _emptyBoxSprite;
        }
    }

    protected virtual void Use()
    {
    }

    private void PlayAnimation()
    {
        if (_animator != null) _animator.SetTrigger("Hit");
    }
}
