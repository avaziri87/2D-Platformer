using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int _points = 100;
    [SerializeField] List<AudioClip> _audioClips;

    public static int CoinsCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) return;
      
        CoinsCollected++;
        ScoreSystem.Add(_points);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            if (_audioClips.Count > 0)
            {
                int rand = Random.Range(0, _audioClips.Count);
                audioSource.PlayOneShot(_audioClips[rand]);
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}
