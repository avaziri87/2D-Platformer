using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int _coinsInBox = 3;

    int _remainingCoins;

    protected override bool CanUse => _remainingCoins > 0;

    void Start()
    {
        _remainingCoins = _coinsInBox;
    }

    protected override void Use()
    {
        _remainingCoins--;
        Coin.CoinsCollected++;
    }
}
