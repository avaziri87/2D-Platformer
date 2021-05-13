using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFireballs : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] float _fireDelay = 0.5f;

    string _fireButton;
    string _horizontalAxis;
    Player _player;
    float _nextFire;

    private void Start()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }
    private void Update()
    {
        if(Input.GetButtonDown(_fireButton) && Time.time >= _fireDelay)
        {
            var horizontal = Input.GetAxis(_horizontalAxis);
            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0 ? 1:-1;
            _nextFire = Time.time + _fireDelay;
        }
    }
}
