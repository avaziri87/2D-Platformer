using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) return;
      
        gameObject.SetActive(false);
        CoinsCollected++;
    }
}
