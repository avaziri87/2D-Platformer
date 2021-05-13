using UnityEngine;

public class Fly : MonoBehaviour, ITakeDamage
{
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2;
    [SerializeField] float _speed = 2;

    Vector2 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);

        var distance = Vector2.Distance(_startPos, transform.position);

        if(distance >= _maxDistance)
        {
            transform.position = _startPos + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
