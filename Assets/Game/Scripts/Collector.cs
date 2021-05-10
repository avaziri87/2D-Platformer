using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectoinComplete;

    TMP_Text _remainingText;

    int _countCollected;

    private void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach(var collectible in _collectibles)
        {
            collectible.OnPikcUp += ItemPickedUp;
        }
        int countRemaining = _collectibles.Count - _countCollected;
        _remainingText?.SetText(countRemaining.ToString());
    }

    public void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;

        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0) return;

        _onCollectoinComplete.Invoke();
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}
