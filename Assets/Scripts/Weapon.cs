using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float xSpeed;
    AudioPlayer audioPlayer;
    

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();   
    }

    void Start()
    {
        xSpeed = player.transform.localScale.x * bulletSpeed;
        audioPlayer.PlayAxeThrowClip();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        audioPlayer.PlayAxeHitClip();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
