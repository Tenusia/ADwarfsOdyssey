using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 100;

    AudioPlayer audioPlayer;

    bool wasCollected = false;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();   
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().ProcessPickup(coinValue);
            audioPlayer.PlayCoinPickupClip();
            Destroy(gameObject);
        }
    }
}
