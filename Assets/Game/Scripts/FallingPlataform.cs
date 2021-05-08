using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataform : MonoBehaviour
{
    [Header("Wiggle Factos")]
    [Tooltip("Reset the wiggle timer if the player leaves the platform")]
    [SerializeField] bool _resetWiggleTimer;
    [Range(0.005f, 0.1f)] [SerializeField] float _wiggleX = 0.05f;
    [Range(0.005f, 0.1f)] [SerializeField] float _wiggleY = 0.05f;
    [Header("Falling Factors")]
    [SerializeField] float _fallSpeed = 3;
    [Range(0.1f, 5)] [SerializeField] float _fallAfterSeconds = 3;


    float _wiggleTimer;
    bool _falling;
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Vector3 _initialPos;

    void Start()
    {
        _initialPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Add(player);

        if(_playersInTrigger.Count ==1) StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        yield return new WaitForSeconds(0.25f);
        
        while(_wiggleTimer < _fallAfterSeconds)
        {
            float randomX = UnityEngine.Random.Range(-_wiggleX, _wiggleX);
            float randomY = UnityEngine.Random.Range(-_wiggleY, _wiggleY);
            transform.position = _initialPos + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            _wiggleTimer += randomDelay;
        }

        _falling = true;

        foreach(var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        float fallTimer = 0;

        while(fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling) return;


        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count < 1)
        {
            StopCoroutine(WiggleAndFall());
            if (_resetWiggleTimer) _wiggleTimer = 0;
        }
    }
}
