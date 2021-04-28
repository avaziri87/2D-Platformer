using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _jumpForce = 200;

    Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();

        if(Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
        }

        var animator = GetComponent<Animator>();
        bool isWalking = horizontal != 0;
        animator.SetBool("Walking", isWalking);

        if(horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPos;
    }
}