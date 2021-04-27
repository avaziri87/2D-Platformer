using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);

        var animator = GetComponent<Animator>();
        bool isWalking = horizontal != 0;
        animator.SetBool("Walking", isWalking);

        if(horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }
    }
}