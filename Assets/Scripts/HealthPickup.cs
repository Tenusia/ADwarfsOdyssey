using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthValue = 1;

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
            FindObjectOfType<GameSession>().ProcessPickupLife(healthValue);
            audioPlayer.PlayHealthPickupClip();
            Destroy(gameObject);
        }
    }
}
