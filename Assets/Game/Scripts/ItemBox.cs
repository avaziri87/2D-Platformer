using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Vector2 _itemLaunchVelocity;

    GameObject _item;
    bool _used;

    protected override bool CanUse => _used == false;

    void Start()
    {
        if (_item != null) _item.SetActive(false);
    }
    protected override void Use()
    {
        _item = Instantiate(_itemPrefab, transform.position + Vector3.up, Quaternion.identity, transform);

        if (_item == null) return;
        
        _used = true;
        _item.SetActive(true);
        var itemRigidbody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = _itemLaunchVelocity;
        }
        
    }
}
