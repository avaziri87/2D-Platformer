using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFireballs : MonoBehaviour
{
    [SerializeField] GameObject _fireballPrefab;

    private void Start()
    {
        Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
    }
}
